using System;
using System.Windows.Forms;
using MyNetworking;

namespace MyServer
{
    public partial class ClientView : Form
    {
        private MyNetworkConnection connection;
        private ZoneMover mover;
        private Random randomName = new Random(Guid.NewGuid().GetHashCode());

        public ClientView()
        {
            InitializeComponent();

            connection = new MyNetworkConnection("Temp: " + randomName.Next(0, 1000));
            connection.OnNetworkError += Connection_OnNetworkError;
            mover = new ZoneMover(connection);

            connection.RegisterHandler(ClientMessageTypes.ReceiveConsoleMessage, OnMessageReceived);
            connection.RegisterHandler(ClientMessageTypes.ConfirmZoneMoveRequest, mover.OnConfirmZoneMoveRequest);
            connection.RegisterHandler(ClientMessageTypes.ReceiveZoneHostNotice, OnReceiveZoneHostNotice);

            connection.SetupConnection();
        }

        public void OnMessageReceived(MyNetworkMessage netMsg)
        {
            //ConsoleTB.AppendText(netMsg.ReadMessage<MyStringMessage>().Value);
            //MethodInvoker update = new MethodInvoker(updateConsole);
            //Invoke(update, netMsg.ReadMessage<MyStringMessage>().Value);
            updateConsole(netMsg.ReadMessage<MyStringMessage>().Value);
        }

        public void OnReceiveZoneHostNotice(MyNetworkMessage netMsg)
        {
            updateConsole("This client is now a room host");
        }

        private void Connection_OnNetworkError(string message)
        {
            //ConsoleTB.AppendText(message);
            updateConsole(message);
        }

        private void updateConsole(string text)
        {
            if (ConsoleTB.InvokeRequired)
                Invoke(new UpdateConsoleHandler(updateConsole), text);
            else
                ConsoleTB.AppendText(text + Environment.NewLine);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            sendConsoleMessage(CommandTB.Text);
            CommandTB.Clear();
        }

        private void sendConsoleMessage(string stringMessage)
        {
            connection.Send(ClientMessageTypes.ReceiveConsoleMessage, new MyStringMessage(stringMessage));
        }

        private void MoveRoomButton_Click(object sender, EventArgs e)
        {
            if (connection.IsConnected)
            {
                if (StressCB.Checked)
                    mover.DelayedZoneMove(RoomTB.Text);
                else
                    mover.RequestZoneMove(RoomTB.Text);
                RoomTB.Clear();
            }

            updateConsole(connection.ClientPort.ToString());
        }

        private void LeaveRoomButton_Click(object sender, EventArgs e)
        {

        }
    }
}
