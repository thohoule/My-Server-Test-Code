using System;

namespace MyNetworking
{
    public abstract class UniversalMessageTypes
    {
        public const short RequestAuthentication = 0;
        public const short ReceiveConsoleMessage = 996;
        public const short ReceiveNetworkError = 999;
    }

    [Serializable]
    public class AuthenticationToken : MyMessageBase
    {
        public string ProfileName;
        public string Address;
        public long Key;
    }
}
