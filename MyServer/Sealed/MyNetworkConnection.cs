using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MyServer
{
    //public class MyNetworkConnection
    //{
    //    private const string LOCAL_ADDRESS = "127.0.0.1";
    //    private const int DEFAULT_PORT = 996;
    //    private const int BUFFER_SIZE = 1024;

    //    private TcpClient client;
    //    private NetworkStream stream;
    //    private Thread thread;
    //    private byte[] buffer;
    //    private Dictionary<short, MyNetworkMessageDelegate> messageHandler;

    //    public string ServerAddress { get; private set; }
    //    public int ServerPort { get; private set; }
    //    public bool IsConnected { get; private set; }

    //    public event NetworkErrorHandler OnNetworkError;

    //    public MyNetworkConnection() : this(LOCAL_ADDRESS, DEFAULT_PORT)
    //    {
    //    }

    //    public MyNetworkConnection(string serverAddress) : this(serverAddress, DEFAULT_PORT)
    //    {
    //    }

    //    public MyNetworkConnection(string serverAddress, int port)
    //    {
    //        ServerAddress = serverAddress;
    //        ServerPort = port;
    //        buffer = new byte[BUFFER_SIZE];
    //        messageHandler = new Dictionary<short, MyNetworkMessageDelegate>();
    //    }

    //    ~MyNetworkConnection()
    //    {
    //        Close();
    //    }

    //    public void SetupConnection()
    //    {
    //        try
    //        {
    //            thread = new Thread(receiveData);
    //            thread.IsBackground = true;
    //            client = new TcpClient(ServerAddress, ServerPort);
    //            stream = client.GetStream();
    //            thread.Start();
    //            IsConnected = true;
    //        }
    //        catch(Exception e)
    //        {
    //            Close();
    //            fireNetworkError(e.Message);
    //        }
    //    }

    //    public void RegisterHandler(short msgType, MyNetworkMessageDelegate handler)
    //    {
    //        messageHandler.Add(msgType, handler);
    //    }

    //    public void Send(short msgType, MyMessageBase message)
    //    {
    //        if (!stream.CanWrite)
    //            return;

    //        byte[] messageData = MyMessageBase.ToByteArray(message);
    //        byte[] formatBuffer = new byte[messageData.Length + sizeof(short)];

    //        Array.Copy(BitConverter.GetBytes(msgType), formatBuffer, sizeof(short));
    //        Array.Copy(messageData, 0, formatBuffer, sizeof(short), messageData.Length);

    //        stream.Write(formatBuffer, 0, formatBuffer.Length);
    //    }

    //    public void Close()
    //    {
    //        if (IsConnected)
    //        {
    //            thread.Interrupt();
    //            stream.Close();
    //            client.Close();
    //            IsConnected = false;
    //        }
    //    }

    //    private void receiveData()
    //    {
    //        if (!IsConnected)
    //            return;

    //        try
    //        {
    //            while (IsConnected && stream.CanRead)
    //            {
    //                var length = stream.Read(buffer, 0, buffer.Length);
    //                stream.Flush();

    //                var rawMessage = new byte[length];
    //                Array.Copy(buffer, rawMessage, length);

    //                //do something with the message
    //                handlePacket(client.Client, rawMessage);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Close();
    //            fireNetworkError(e.Message);
    //        }
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

    //    private void fireNetworkError(string message)
    //    {
    //        if (OnNetworkError != null)
    //            OnNetworkError.Invoke(message);
    //    }
    //}
}
