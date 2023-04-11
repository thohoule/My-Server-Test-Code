using System;
using System.Net.Sockets;

namespace MyNetworking
{
    public class ClientSocket
    {
        public Socket ClientsSocket { get; private set; }
        public string Name { get; private set; }

        //public ClientSocket(Socket socket)
        //{
        //    ClientsSocket = socket;
        //}

        public ClientSocket(Socket socket, string name)
        {
            ClientsSocket = socket;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, ClientsSocket.RemoteEndPoint.ToString());
        }
    }
}
