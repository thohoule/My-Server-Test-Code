using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyNetworking;

namespace MyServer
{
    public delegate void UpdateConsoleHandler(string text);

    public partial class ServerView : Form
    {
        private MyServerManager server;
        private ZoneRoomManager roomManager;

        private delegate void UpdateConnectionsHandler(ICollection<ClientSocket> clients);
        private delegate void UpdateRoomHandler(TransitionalRoom waitingRoom, ICollection<ZoneRoom> rooms);

        public ServerView()
        {
            InitializeComponent();

            server = MyServerManager.Instance;
            roomManager = ZoneRoomManager.Instance;

            server.ClientAdded += Instance_OnClientAdded;
            server.ClientRemoved += Instance_OnClientRemoved;
            server.OnNetworkError += Instance_OnNetworkError;
            server.ServerStatusChanged += Server_ServerStatusChanged;

            roomManager.RoomAdded += RoomManager_RoomAdded;
            roomManager.RoomRemoved += RoomManager_RoomRemoved;
            roomManager.RoomChanged += RoomManager_RoomChanged;

            server.RegisterHandler(MyServerMessageTypes.ReceiveConsoleMessage, onReceiveConsoleMessage);

            server.RegisterHandler(MyServerMessageTypes.RequestForZoneInfo, roomManager.OnZoneInfoRequest);
            server.RegisterHandler(MyServerMessageTypes.ClientBeginMovingZone, roomManager.OnClientBeginMovingZone);
            server.RegisterHandler(MyServerMessageTypes.ClientDoneMovingZone, roomManager.OnClientDoneMovingZone);
            server.RegisterHandler(MyServerMessageTypes.RequestPlayerZoneLocation, roomManager.OnRequestPlayerZoneLocation);
            server.RegisterHandler(MyServerMessageTypes.RequestingZoneHostPosition, roomManager.OnRequestingZoneHostPosition);

            server.Start();
        }

        private void RoomManager_RoomAdded(RoomChangeHandlerArgs args)
        {
            updateConsole("Room opened. Host: " + args.MovedClient);
            //updateRooms(args.CurrentRooms);
        }

        private void RoomManager_RoomRemoved(RoomChangeHandlerArgs args)
        {
            updateConsole("Room closed. Last Host: " + args.MovedClient);
            //updateRooms(args.CurrentRooms);
        }

        private void RoomManager_RoomChanged(RoomChangeHandlerArgs args)
        {
            updateConsole("Player has moved to room: " + args.ChangedRoom.ToString());
            updateRooms(args.WaitingRoom, args.CurrentRooms);
        }

        private void Instance_OnClientAdded(ClientsChangedHandlerArg arg)
        {
            updateConsole("Client Connected: " + arg.ChangedClient.ToString());
            buildConnetionListBox(arg.CurrentClients);
        }

        private void Instance_OnClientRemoved(ClientsChangedHandlerArg arg)
        {
            updateConsole("Client Disconnected: " + arg.ChangedClient.ToString());
            buildConnetionListBox(arg.CurrentClients);
        }

        private void Instance_OnNetworkError(string message)
        {
            updateConsole(message);
        }

        private void Server_ServerStatusChanged(string message)
        {
            ServerStatusLabel.Text = message;
        }

        private void onReceiveConsoleMessage(MyNetworkMessage netMsg)
        {
            updateConsole(netMsg.ReadMessage<MyStringMessage>().Value);
        }

        private void updateConsole(string text)
        {
            if (ConsoleTB.InvokeRequired)
            {
                UpdateConsoleHandler d = new UpdateConsoleHandler(updateConsole);
                Invoke(d, text);
            }
            else
                ConsoleTB.AppendText(text + Environment.NewLine);
        }

        private void buildConnetionListBox(ICollection<ClientSocket> clients)
        {
            if (ConnectionsLB.InvokeRequired)
                Invoke(new UpdateConnectionsHandler(buildConnetionListBox), clients);
            else
            {
                ConnectionsLB.Items.Clear();

                foreach (ClientSocket client in clients)
                {
                    ConnectionsLB.Items.Add(client);
                }
            }
        }

        private void updateRooms(TransitionalRoom waitingRoom, ICollection<ZoneRoom> rooms)
        {
            if (RoomsTV.InvokeRequired)
                Invoke(new UpdateRoomHandler(updateRooms), waitingRoom, rooms);
            else
            {
                RoomsTV.Nodes.Clear();

                var waitNode = RoomsTV.Nodes.Add("Waiting Room");

                foreach (ClientSocket client in waitingRoom.ClientsInZone.Keys)
                {
                    waitNode.Nodes.Add(client.Name);
                }

                foreach (ZoneRoom room in rooms)
                {
                    var node = RoomsTV.Nodes.Add(room.ToString());
                    foreach (ClientSocket client in room.ClientsInZone)
                    {
                        var branch = node.Nodes.Add(client.Name);
                        if (client == room.ZoneHost)
                            branch.ForeColor = System.Drawing.Color.Blue;
                    }
                }

                RoomsTV.ExpandAll();
            }
        }

        private void BroadcastButton_Click(object sender, EventArgs e)
        {
            var text = tbCommandLine.Text;
            tbCommandLine.Clear();

            foreach (ClientSocket client in server.Clients)
            {
                server.SendTo(client.ClientsSocket, UniversalMessageTypes.ReceiveConsoleMessage, new MyStringMessage(text));
            }
        }
    }
}
