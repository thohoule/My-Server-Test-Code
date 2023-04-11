using System;

namespace MyNetworking
{
    public sealed class MyAuthorizedNetworkConnection : MyNetworkConnection
    {
        public long TokenKey { get; private set; }

        public MyAuthorizedNetworkConnection(string profileName) : base(profileName)
        {
        }

        public MyAuthorizedNetworkConnection(string profileName, string serverAddress) : base(profileName, serverAddress)
        {
        }

        public MyAuthorizedNetworkConnection(string profileName, string serverAddress, int port) : base(profileName, serverAddress, port)
        {
        }

        ~MyAuthorizedNetworkConnection()
        {
            Close();
        }
    }
}
