using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNetworking;

namespace MyServer
{
    public delegate void NewZoneHostHandler(ClientSocket client);

    public interface IZoneRoom
    {

    }

    public class ZoneRoom : IZoneRoom
    {
        public string ZoneName { get; private set; }
        public ClientSocket ZoneHost { get; private set; }
        public HashSet<ClientSocket> ClientsInZone { get; private set; }
        public bool IsEmpty { get { return ClientsInZone.Count == 0; } }

        public event NewZoneHostHandler OnNewHost;

        public ZoneRoom(string zoneName, ClientSocket client) : this(zoneName, client, new HashSet<ClientSocket>() { client })
        {
        }

        public ZoneRoom(string zoneName, ClientSocket host, HashSet<ClientSocket> clients)
        {
            if (zoneName == null || zoneName == "" || host == null || clients == null)
                throw new ArgumentNullException();

            ZoneName = zoneName;
            ZoneHost = host;
            ClientsInZone = clients;

            if (!clients.Contains(host))
                clients.Add(host);

            fireNewHost(host);
        }

        public void PromoteClientToHost(ClientSocket client)
        {
            if (ClientsInZone.Contains(client))
            {
                ZoneHost = client;
                fireNewHost(client);
            }
        }

        public void RemoveClient(ClientSocket client)
        {
            if (ZoneHost == client && ClientsInZone.Count > 1)
                simpleHostRotate(client);
            else
                ClientsInZone.Remove(client);
        }

        public override string ToString()
        {
            return ZoneName;
        }

        private void simpleHostRotate(ClientSocket client)
        {
            ClientsInZone.Remove(client);
            ZoneHost = ClientsInZone.First();
            fireNewHost(ZoneHost);
        }

        private void fireNewHost(ClientSocket client)
        {
            if (OnNewHost != null)
                OnNewHost.Invoke(client);
        }
    }

    public class TransitionalRoom : IZoneRoom
    {
        /// <summary>
        /// String is the destination scene/room.
        /// </summary>
        public Dictionary<ClientSocket, string> ClientsInZone { get; private set; }

        public TransitionalRoom()
        {
            ClientsInZone = new Dictionary<ClientSocket, string>();
        }
    }
}
