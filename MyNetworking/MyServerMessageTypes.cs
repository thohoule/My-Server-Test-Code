using System;
using System.Net;
using MyNetworking;

namespace MyNetworking
{
    public sealed class MyServerMessageTypes : UniversalMessageTypes
    {
        //0 is an internal server authentication message
        public const short RequestForZoneInfo = 1;
        public const short ClientBeginMovingZone = 2;
        public const short ClientDoneMovingZone = 3;
        public const short RequestPlayerZoneLocation = 4;
        public const short RequestingZoneHostPosition = 5;

        public const short UpdatePlayerInfo = 20;

    }

    [Serializable]
    public class ZoneInfoMessage : MyMessageBase
    {
        public bool HasHost;
        public string ProfileName;
        public string HostAddress;
        public int HostPort;

        public ZoneInfoMessage()
        {

        }

        public ZoneInfoMessage(bool hasHost, string profileName, IPEndPoint endpoint) : this(hasHost, profileName, endpoint.Address.ToString(), endpoint.Port)
        {
        }

        public ZoneInfoMessage(bool hasHost, string profileName, string address, int port)
        {
            HasHost = hasHost;
            if (hasHost)
            {
                ProfileName = profileName;
                HostAddress = address;
                HostPort = port;
            }
        }
    }
}
