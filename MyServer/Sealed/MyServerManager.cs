using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MyServer
{
    //public delegate void ClientsChangedHandler(ClientsChangedHandlerArg arg);
    //public delegate void NetworkErrorHandler(string message);
    //public delegate void ConsoleMessageHandler(string message);

    //public class MyServerManager
    //{
    //    private const int BUFFER_SIZE = 1024;
    //    private const int DEFAULT_PORT = 996;

    //    private const string OFFLINE_MESSAGE = "Offline";
    //    private const string STARTING_MESSAGE = "Starting";
    //    private const string RUNNING_MESSAGE = "Running";

    //    private Socket serverSocket;
    //    private List<ClientSocket> clients;
    //    private byte[] buffer;
    //    private Dictionary<short, MyNetworkMessageDelegate> messageHandler;
    //    private bool hasStarted;

    //    public List<ClientSocket> Clients { get { return clients; } }

    //    public event ClientsChangedHandler ClientAdded;
    //    public event ClientsChangedHandler ClientRemoved;
    //    public event NetworkErrorHandler OnNetworkError;
    //    public event ConsoleMessageHandler ServerStatusChanged;

    //    private static MyServerManager instance;
    //    public static MyServerManager Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new MyServerManager();

    //            return instance;
    //        }
    //    }

    //    private MyServerManager()
    //    {
    //        clients = new List<ClientSocket>();
    //        buffer = new byte[BUFFER_SIZE];
    //        messageHandler = new Dictionary<short, MyNetworkMessageDelegate>();
    //        fireServerStatusChange(OFFLINE_MESSAGE);

    //        //serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //        //serverSocket.Bind(new IPEndPoint(IPAddress.Any, DEFAULT_PORT));
    //        //serverSocket.Listen(1);
    //        //serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
    //    }

    //    public void Start()
    //    {
    //        if (hasStarted)
    //            return;

    //        fireServerStatusChange(STARTING_MESSAGE);

    //        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //        serverSocket.Bind(new IPEndPoint(IPAddress.Any, DEFAULT_PORT));
    //        serverSocket.Listen(1);
    //        serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
    //        hasStarted = true;
    //        fireServerStatusChange(RUNNING_MESSAGE);
    //    }

    //    public void RegisterHandler(short msgType, MyNetworkMessageDelegate handler)
    //    {
    //        messageHandler.Add(msgType, handler);
    //    }

    //    public void SendTo(Socket socket, short msgType, MyMessageBase message)
    //    {
    //        if (!hasStarted)
    //            throw new InvalidOperationException("Server needs to be started before this can be called.");

    //        byte[] messageData = MyMessageBase.ToByteArray(message);
    //        byte[] formatBuffer = new byte[messageData.Length + sizeof(short)];

    //        Array.Copy(BitConverter.GetBytes(msgType), formatBuffer, sizeof(short));
    //        Array.Copy(messageData, 0, formatBuffer, sizeof(short), messageData.Length);

    //        socket.BeginSend(formatBuffer, 0, formatBuffer.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
    //        serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
    //    }

    //    private void acceptCallback(IAsyncResult arg)
    //    {
    //        Socket socket = serverSocket.EndAccept(arg);
    //        var client = new ClientSocket(socket);

    //        clients.Add(client);
    //        fireClientAddedEvent(client);

    //        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
    //        serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
    //    }

    //    private void receiveCallback(IAsyncResult arg)
    //    {
    //        var socket = (Socket)arg.AsyncState;

    //        if (socket.Connected)
    //        {
    //            int receivedBytes = 0;

    //            try
    //            {
    //                receivedBytes = socket.EndReceive(arg);
    //            }
    //            catch (Exception)
    //            {
    //                for (int index = 0; index < clients.Count; index++)
    //                {
    //                    if (clients[index].ClientsSocket.RemoteEndPoint.Equals(socket.RemoteEndPoint))
    //                    {
    //                        handleDisconnectedClient(clients[index]);
    //                        return;
    //                    }
    //                }
    //            }

    //            if (receivedBytes != 0)
    //            {
    //                byte[] dataBuffer = new byte[receivedBytes];
    //                Array.Copy(buffer, dataBuffer, receivedBytes);

    //                handlePacket(socket, dataBuffer);
    //            }
    //            else
    //            {
    //                for (int index = 0; index < clients.Count; index++)
    //                {
    //                    if (clients[index].ClientsSocket.RemoteEndPoint.Equals(socket.RemoteEndPoint))
    //                    {
    //                        handleDisconnectedClient(clients[index]);
    //                    }
    //                }
    //            }

    //            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
    //        }
    //    }

    //    private void sendCallback(IAsyncResult arg)
    //    {
    //        Socket socket = (Socket)arg.AsyncState;
    //        socket.EndSend(arg);
    //    }

    //    private void handlePacket(Socket socket, byte[] data)
    //    {
    //        MyNetworkMessage netmsg = new MyNetworkMessage(socket, data);

    //        try
    //        {
    //            messageHandler[netmsg.MsgType].Invoke(netmsg);
    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            fireNetworkError("Unhandled Packet Error: Msgtype not found, please register a handler.");
    //        }
    //    }

    //    private void handleDisconnectedClient(ClientSocket client)
    //    {
    //        clients.Remove(client);
    //        fireClientRemovedEvent(client);
    //    }

    //    private void fireClientAddedEvent(ClientSocket client)
    //    {
    //        if (ClientAdded != null)
    //            ClientAdded.Invoke(new ClientsChangedHandlerArg(client, clients));
    //    }

    //    private void fireClientRemovedEvent(ClientSocket client)
    //    {
    //        if (ClientRemoved != null)
    //            ClientRemoved.Invoke(new ClientsChangedHandlerArg(client, clients));
    //    }

    //    private void fireNetworkError(string message)
    //    {
    //        if (OnNetworkError != null)
    //            OnNetworkError.Invoke(message);
    //    }

    //    private void fireServerStatusChange(string status)
    //    {
    //        if (ServerStatusChanged != null)
    //            ServerStatusChanged.Invoke(status);
    //    }
    //}

    //public class ClientSocket
    //{
    //    public Socket ClientsSocket { get; private set; }
    //    public string Name { get; set; }

    //    public ClientSocket(Socket socket)
    //    {
    //        ClientsSocket = socket;
    //    }

    //    public override string ToString()
    //    {
    //        return ClientsSocket.RemoteEndPoint.ToString();
    //    }
    //}

    //public struct ClientsChangedHandlerArg
    //{
    //    public ClientSocket ChangedClient { get; private set; }
    //    public List<ClientSocket> CurrentClients { get; private set; }

    //    public ClientsChangedHandlerArg(ClientSocket changedClient, List<ClientSocket> currentClients)
    //    {
    //        ChangedClient = changedClient;
    //        CurrentClients = currentClients;
    //    }
    //}
}
