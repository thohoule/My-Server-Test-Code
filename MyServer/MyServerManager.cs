using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MyNetworking;

namespace MyServer
{
    public delegate void ClientsChangedHandler(ClientsChangedHandlerArg arg);

    public class MyServerManager
    {
        private const int BUFFER_SIZE = 1024;
        private const int DEFAULT_PORT = 996;

        private const string OFFLINE_MESSAGE = "Offline";
        private const string STARTING_MESSAGE = "Starting";
        private const string RUNNING_MESSAGE = "Running";

        private Socket serverSocket;
        private HashSet<Socket> unregisteredClients;
        private Dictionary<Socket, ClientSocket> clients;
        private Dictionary<string, Socket> socketLookup;
        private byte[] buffer;
        private Dictionary<short, MyNetworkMessageDelegate> messageHandler;
        private bool hasStarted;

        public List<ClientSocket> Clients { get { return clients.Values.ToList(); } }
        //public Dictionary<Socket, string> Clients { get; private set; }
        //public Dictionary<string, Socket> ClientsByName { get; private set; }

        public event ClientsChangedHandler ClientAdded;
        public event ClientsChangedHandler ClientRemoved;
        public event NetworkErrorHandler OnNetworkError;
        public event ConsoleMessageHandler ServerStatusChanged;

        private static MyServerManager instance;
        public static MyServerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MyServerManager();

                return instance;
            }
        }

        private MyServerManager()
        {
            unregisteredClients = new HashSet<Socket>();
            clients = new Dictionary<Socket, ClientSocket>();
            socketLookup = new Dictionary<string, Socket>();
            buffer = new byte[BUFFER_SIZE];
            messageHandler = new Dictionary<short, MyNetworkMessageDelegate>();
            messageHandler.Add(0, authenticateClient);
            fireServerStatusChange(OFFLINE_MESSAGE);
        }

        public void Start()
        {
            if (hasStarted)
                return;

            fireServerStatusChange(STARTING_MESSAGE);

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, DEFAULT_PORT));
            serverSocket.Listen(1);
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
            hasStarted = true;
            fireServerStatusChange(RUNNING_MESSAGE);
        }

        public void RegisterHandler(short msgType, MyNetworkMessageDelegate handler)
        {
            messageHandler.Add(msgType, handler);
        }

        public void SendTo(Socket socket, short msgType, MyMessageBase message)
        {
            if (!hasStarted)
                throw new InvalidOperationException("Server needs to be started before this can be called.");

            byte[] messageData = MyMessageBase.ToByteArray(message);
            byte[] formatBuffer = new byte[messageData.Length + sizeof(short)];

            Array.Copy(BitConverter.GetBytes(msgType), formatBuffer, sizeof(short));
            Array.Copy(messageData, 0, formatBuffer, sizeof(short), messageData.Length);

            socket.BeginSend(formatBuffer, 0, formatBuffer.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        public ClientSocket GetClient(Socket socket)
        {
            return clients[socket];
        }

        public ClientSocket GetClientByProfileName(string name)
        {
            return GetClient(socketLookup[name]);
        }

        public bool TryGetClient(Socket socket, out ClientSocket client)
        {
            return clients.TryGetValue(socket, out client);
        }

        public bool TryGetClientByProfileName(string name, out ClientSocket client)
        {
            client = null;
            Socket socket;
            if (socketLookup.TryGetValue(name, out socket))
                return TryGetClient(socket, out client);
            else
                return false;
        }

        private void acceptCallback(IAsyncResult arg)
        {
            Socket socket = serverSocket.EndAccept(arg);
            //var client = new ClientSocket(socket);

            unregisteredClients.Add(socket);
            //fireClientAddedEvent(client);

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        private void receiveCallback(IAsyncResult arg)
        {
            var socket = (Socket)arg.AsyncState;

            if (socket.Connected)
            {
                int receivedBytes = 0;

                try
                {
                    receivedBytes = socket.EndReceive(arg);
                }
                catch (Exception)
                {
                    if (clients.ContainsKey(socket))
                        handleDisconnectedClient(socket);
                    else if (unregisteredClients.Contains(socket))
                        unregisteredClients.Remove(socket);

                    return;
                }

                if (receivedBytes != 0)
                {
                    byte[] dataBuffer = new byte[receivedBytes];
                    Array.Copy(buffer, dataBuffer, receivedBytes);

                    handlePacket(socket, dataBuffer);
                }
                else
                {
                    if (clients.ContainsKey(socket))
                        handleDisconnectedClient(socket);
                    else if (unregisteredClients.Contains(socket))
                        unregisteredClients.Remove(socket);
                }

                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
            }
        }

        private void sendCallback(IAsyncResult arg)
        {
            Socket socket = (Socket)arg.AsyncState;
            socket.EndSend(arg);
        }

        private void handlePacket(Socket socket, byte[] data)
        {
            MyNetworkMessage netmsg = new MyNetworkMessage(socket, data);

            if (unregisteredClients.Contains(socket) && netmsg.MsgType != 0)
                return; //message ignored

            try
            {
                messageHandler[netmsg.MsgType].Invoke(netmsg);
            }
            catch (KeyNotFoundException)
            {
                fireNetworkError("Unhandled Packet Error: Msgtype not found, please register a handler.");
            }
        }

        private void authenticateClient(MyNetworkMessage netMsg)
        {
            var token = netMsg.ReadMessage<AuthenticationToken>();

            //TODO: requst second token from profile server and match info

            //bypass
            if (unregisteredClients.Remove(netMsg.Connection))
            {
                try
                {
                    var client = new ClientSocket(netMsg.Connection, token.ProfileName);
                    clients.Add(client.ClientsSocket, client);
                    socketLookup.Add(client.Name, client.ClientsSocket);
                    fireClientAddedEvent(client);
                }
                catch (ArgumentException e)
                {
                    SendTo(netMsg.Connection, 999, new MyStringMessage(e.ToString()));
                }
            }
        }

        private void handleDisconnectedClient(Socket client)
        {
            var value = clients[client];
            clients.Remove(client);
            socketLookup.Remove(value.Name);
            fireClientRemovedEvent(value);
        }

        private void fireClientAddedEvent(ClientSocket client)
        {
            if (ClientAdded != null)
                ClientAdded.Invoke(new ClientsChangedHandlerArg(client, clients.Values));
        }

        private void fireClientRemovedEvent(ClientSocket client)
        {
            if (ClientRemoved != null)
                ClientRemoved.Invoke(new ClientsChangedHandlerArg(client, clients.Values));
        }

        private void fireNetworkError(string message)
        {
            if (OnNetworkError != null)
                OnNetworkError.Invoke(message);
        }

        private void fireServerStatusChange(string status)
        {
            if (ServerStatusChanged != null)
                ServerStatusChanged.Invoke(status);
        }
    }

    public struct ClientsChangedHandlerArg
    {
        public ClientSocket ChangedClient { get; private set; }
        public ICollection<ClientSocket> CurrentClients { get; private set; }

        public ClientsChangedHandlerArg(ClientSocket changedClient, ICollection<ClientSocket> currentClients)
        {
            ChangedClient = changedClient;
            CurrentClients = currentClients;
        }
    }
}
