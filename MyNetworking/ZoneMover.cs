using System;
using System.Collections.Generic;
using System.Threading;

namespace MyNetworking
{
    public delegate void SceneLoaderHandler(string sceneName, ZoneInfoMessage info);

    public class ZoneMover
    {
        private MyNetworkConnection connection;
        private SceneLoaderHandler sceneLoader;
        //private string current;
        private string destination;
        private bool delay;

        public bool ReadyToMove { get; private set; }

        public ZoneMover(MyNetworkConnection connection)
        {
            this.connection = connection;
            ReadyToMove = true;
        }

        public ZoneMover(MyNetworkConnection connection, SceneLoaderHandler sceneLoader)
        {
            this.connection = connection;
            this.sceneLoader = sceneLoader;
            ReadyToMove = true;
        }

        public void RequestZoneMove(string destination)
        {
            if (!connection.IsConnected)
                throw new InvalidOperationException("Connection isn't online.");

            if (!ReadyToMove)
                return;//can confirm zone move here

            ReadyToMove = false;

            this.destination = destination;
            connection.Send(MyServerMessageTypes.ClientBeginMovingZone, new ClientZoneTransferMessage(connection.ProfileName, destination));
        }

        public void DelayedZoneMove(string destination)
        {
            delay = true;
            RequestZoneMove(destination);
        }

        public void OnConfirmZoneMoveRequest(MyNetworkMessage netMsg)
        {
            if (ReadyToMove)
                return;

            if (delay)
                System.Threading.Thread.Sleep(10000);

            var message = netMsg.ReadMessage<ZoneInfoMessage>();

            if (sceneLoader == null)
                connection.Send(MyServerMessageTypes.ClientDoneMovingZone, new ClientZoneTransferMessage(connection.ProfileName, destination));
            else
                sceneLoader.Invoke(destination, message);

            ReadyToMove = true;
        }
    }
}
