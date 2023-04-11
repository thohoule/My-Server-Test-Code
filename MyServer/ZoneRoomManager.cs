using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using MyNetworking;

namespace MyServer
{
    public delegate void RoomChangeHandler(RoomChangeHandlerArgs args);

    public class ZoneRoomManager
    {
        private MyServerManager server;
        private Dictionary<string, ZoneRoom> rooms;
        private Dictionary<ClientSocket, string> locations;
        private TransitionalRoom transitionalRoom;
        private Dictionary<ClientSocket, ClientZoneTransfer> waitingToMove; //waiting on, the one waiting

        public event RoomChangeHandler RoomAdded;
        public event RoomChangeHandler RoomRemoved;
        public event RoomChangeHandler RoomChanged;

        private static ZoneRoomManager instance;
        public static ZoneRoomManager Instance { get { return instance ?? (instance = new ZoneRoomManager()); } }

        private ZoneRoomManager()
        {
            server = MyServerManager.Instance;
            rooms = new Dictionary<string, ZoneRoom>();
            locations = new Dictionary<ClientSocket, string>();
            transitionalRoom = new TransitionalRoom();
            waitingToMove = new Dictionary<ClientSocket, ClientZoneTransfer>();

            server.ClientAdded += Server_ClientAdded;
            server.ClientRemoved += Server_ClientRemoved;
        }

        //~ZoneRoomManager()
        //{
        //    server.ClientRemoved -= Server_ClientRemoved;
        //}

        public void OnZoneInfoRequest(MyNetworkMessage netMsg)
        {
            var zoneName = netMsg.ReadMessage<MyStringMessage>().Value;

            if (zoneName == null || zoneName == "")
            {
                sendNetworkErrorMessage(netMsg.Connection, "Zone name received is null or empty.");
                return;
            }

            sendZoneInfo(zoneName, netMsg.Connection, ClientMessageTypes.ReceiveZoneInfo);

            //ZoneRoom room;
            //if (rooms.TryGetValue(zoneName, out room))
            //    server.SendTo(netMsg.Connection, ClientMessageTypes.ReceiveZoneInfo,
            //        new ZoneInfoMessage(true, room.ZoneHost.ClientsSocket.RemoteEndPoint.ToString(), 
            //        room.ZoneHost.Name));
            //else
            //    server.SendTo(netMsg.Connection, ClientMessageTypes.ReceiveZoneInfo,
            //        new ZoneInfoMessage());
        }

        public void OnClientBeginMovingZone(MyNetworkMessage netMsg)
        {
            var message = netMsg.ReadMessage<ClientZoneTransferMessage>();

            if (!message.NoNullOrEmpty)
            {
                sendNetworkErrorMessage(netMsg.Connection, "Message contained null or empty fleilds.");
                return;
            }

            try
            {
                var client = server.GetClient(netMsg.Connection);

                if (!locations.ContainsKey(client))
                {
                    sendZoneInfo(message.DestinationZone, client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest);
                    ////transitionalRoom.ClientsInZone.Add(client, message.DestinationZone);
                    //server.SendTo(client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest,
                    //    new MyBlankMessage());
                    ////fireRoomChanged(client, transitionalRoom);
                    return;
                }

                string location = locations[client];

                if (removeFromRoom(client, location, message.DestinationZone))
                    sendZoneInfo(message.DestinationZone, client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest);
                    //server.SendTo(client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest, 
                    //    new MyBlankMessage());
            }
            catch (KeyNotFoundException)
            {
                sendNetworkErrorMessage(netMsg.Connection, "Client's profile name or location could not be found. May Also Already be moving.");
            }

            //rooms[location].ClientsInZone.Remove(client);//handle collasping rooms
            //locations.Remove(client);
        }

        public void OnClientDoneMovingZone(MyNetworkMessage netMsg)
        {
            var message = netMsg.ReadMessage<ClientZoneTransferMessage>();

            if (!message.NoNullOrEmpty)
            {
                sendNetworkErrorMessage(netMsg.Connection, "Message contained null or empty fleilds.");
                return;
            }

            var client = server.GetClient(netMsg.Connection);

            transitionalRoom.ClientsInZone.Remove(client);
            var room = addToRoom(message.DestinationZone, client);
            //fireRoomChanged(client, room);

            ClientZoneTransfer waiting;
            if (waitingToMove.TryGetValue(client, out waiting))
            {
                waitingToMove.Remove(client);
                if (removeFromRoom(waiting))
                    sendZoneInfo(waiting.Destination, waiting.Client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest);
                    //server.SendTo(waiting.Client.ClientsSocket, ClientMessageTypes.ConfirmZoneMoveRequest,
                    //    new MyBlankMessage());
            }
        }

        public void OnRequestPlayerZoneLocation(MyNetworkMessage netMsg)
        {
            var profileName = netMsg.ReadMessage<MyStringMessage>().Value;

            ClientSocket client;
            if (server.TryGetClientByProfileName(profileName, out client))
            {
                string location;
                if (locations.TryGetValue(client, out location))
                    server.SendTo(netMsg.Connection, ClientMessageTypes.ReceiveClientLocation, 
                        new MyStringMessage(location));
                else
                    sendNetworkErrorMessage(netMsg.Connection, "Unable to find that clients location.");
            }
            else
                sendNetworkErrorMessage(netMsg.Connection, "Unable to find client by that name.");
        }

        public void OnRequestingZoneHostPosition(MyNetworkMessage netMsg)
        {
            ClientSocket client = server.GetClient(netMsg.Connection);

            string location;
            ZoneRoom room;
            if (locations.TryGetValue(client, out location))
            {
                if (rooms.TryGetValue(location, out room))
                    room.PromoteClientToHost(client);
            }
            else
                sendNetworkErrorMessage(netMsg.Connection, "Unable to find client's location");
        }

        private void sendZoneInfo(string zoneName, Socket socket, short msgType)
        {
            ZoneRoom room;
            if (rooms.TryGetValue(zoneName, out room))
                server.SendTo(socket, msgType,
                    new ZoneInfoMessage(true, room.ZoneHost.Name, room.ZoneHost.ClientsSocket.RemoteEndPoint as IPEndPoint));
            else
                server.SendTo(socket, msgType,
                    new ZoneInfoMessage());
        }

        private ZoneRoom addToRoom(string zoneName, ClientSocket client)
        {
            ZoneRoom room;
            if (rooms.TryGetValue(zoneName, out room))
            {
                room.ClientsInZone.Add(client);
            }
            else
            {
                room = new ZoneRoom(zoneName, client);
                rooms.Add(zoneName, room);
                fireRoomAdded(client, room);
                room.OnNewHost += Room_OnNewHost;
                sendZoneHostNotice(client.ClientsSocket);
            }

            locations.Add(client, zoneName);
            fireRoomChanged(client, room);

            return room;
        }

        private bool removeFromRoom(ClientSocket client, string location, string destination)
        {
            return removeFromRoom(new ClientZoneTransfer(client, location, destination));
        }

        private bool removeFromRoom(ClientZoneTransfer info)
        {
            if (waitingToMove.ContainsValue(info))
                return false;

            if (rooms[info.CurrentLocation].ZoneHost == info.Client)
            {
                foreach (KeyValuePair<ClientSocket, string> loadingClient in transitionalRoom.ClientsInZone)
                {
                    if (loadingClient.Value == info.CurrentLocation)
                    {
                        waitingToMove.Add(loadingClient.Key, info);
                        return false;
                    }
                }
            }

            transitionalRoom.ClientsInZone.Add(info.Client, info.Destination);
            forceRemove(info.Client, info.CurrentLocation);
            //locations.Remove(info.Client);

            //var room = rooms[info.CurrentLocation];
            //room.RemoveClient(info.Client);
            //if (room.IsEmpty)
            //{
            //    rooms.Remove(info.CurrentLocation);
            //    fireRoomRemove(room);
            //}

            return true;
        }

        private void forceRemove(ClientSocket client)
        {
            string location;
            if (locations.TryGetValue(client, out location))
            {
                forceRemove(client, location);
            }
            else if (transitionalRoom.ClientsInZone.ContainsKey(client))
            {
                transitionalRoom.ClientsInZone.Remove(client);
                fireRoomChanged(client, transitionalRoom);
            }
        }

        private void forceRemove(ClientSocket client, string location)
        {
            locations.Remove(client);

            var room = rooms[location];
            room.RemoveClient(client);
            if (room.IsEmpty)
            {
                room.OnNewHost -= Room_OnNewHost;
                rooms.Remove(location);
                fireRoomRemove(client, room);
            }

            fireRoomChanged(client, room);
        }

        private void sendNetworkErrorMessage(Socket socket, string message)
        {
            server.SendTo(socket, ClientMessageTypes.ReceiveNetworkError,
                    new MyStringMessage(message));
        }

        private void sendZoneHostNotice(Socket socket)
        {
            server.SendTo(socket, ClientMessageTypes.ReceiveZoneHostNotice, new MyBlankMessage());
        }

        private void Room_OnNewHost(ClientSocket client)
        {
            sendZoneHostNotice(client.ClientsSocket);
        }

        private void Server_ClientAdded(ClientsChangedHandlerArg arg)
        {
            transitionalRoom.ClientsInZone.Add(arg.ChangedClient, "");
            fireRoomChanged(arg.ChangedClient, transitionalRoom);
        }

        private void Server_ClientRemoved(ClientsChangedHandlerArg arg)
        {
            forceRemove(arg.ChangedClient);
        }

        private void fireRoomAdded(ClientSocket client, ZoneRoom room)
        {
            if (RoomAdded != null)
                RoomAdded.Invoke(new RoomChangeHandlerArgs(client, room, transitionalRoom, rooms.Values));
        }

        private void fireRoomRemove(ClientSocket client, ZoneRoom room)
        {
            if (RoomRemoved != null)
                RoomRemoved.Invoke(new RoomChangeHandlerArgs(client, room, transitionalRoom, rooms.Values));
        }

        private void fireRoomChanged(ClientSocket client, IZoneRoom room)
        {
            if (RoomChanged != null)
                RoomChanged.Invoke(new RoomChangeHandlerArgs(client, room, transitionalRoom, rooms.Values));
        }
    }

    public class RoomChangeHandlerArgs
    {
        public ClientSocket MovedClient { get; private set; }

        public IZoneRoom ChangedRoom { get; private set; }
        public TransitionalRoom WaitingRoom { get; private set; }
        public ICollection<ZoneRoom> CurrentRooms { get; private set; }

        public RoomChangeHandlerArgs(IZoneRoom changedRoom, TransitionalRoom waitingRoom, ICollection<ZoneRoom> currentRooms) : this (null, changedRoom, waitingRoom, currentRooms)
        {
            //ChangedRoom = changedRoom;
            //CurrentRooms = currentRooms;
        }

        public RoomChangeHandlerArgs(ClientSocket movedClient, IZoneRoom changedRoom, TransitionalRoom waitingRoom, ICollection<ZoneRoom> currentRooms)
        {
            MovedClient = movedClient;
            ChangedRoom = changedRoom;
            WaitingRoom = waitingRoom;
            CurrentRooms = currentRooms;
        }
    }

    public struct ClientZoneTransfer
    {
        public ClientSocket Client { get; set; }
        public string CurrentLocation { get; set; }
        public string Destination { get; set; }

        public ClientZoneTransfer(ClientSocket client, string current, string destination)
        {
            Client = client;
            CurrentLocation = current;
            Destination = destination;
        }
    }
}
