using System;

namespace MyNetworking
{
    public sealed class ClientMessageTypes : UniversalMessageTypes
    {
        public const short ReceiveZoneInfo = 400;
        public const short ConfirmZoneMoveRequest = 401;
        public const short ReceiveClientLocation = 402;
        public const short ReceiveZoneHostNotice = 403;
    }

    [Serializable]
    public class ClientZoneTransferMessage : MyMessageBase
    {
        public string ProfileName;
        public string DestinationZone;

        public bool NoNullOrEmpty
        {
            get
            {
                if (ProfileName == null || DestinationZone == null)
                    return false;
                if (ProfileName == "" || DestinationZone == "")
                    return false;

                return true;
            }
        }

        public ClientZoneTransferMessage()
        {
        }

        public ClientZoneTransferMessage(string profileName, string destination)
        {
            ProfileName = profileName;
            DestinationZone = destination;
        }
    }
}
