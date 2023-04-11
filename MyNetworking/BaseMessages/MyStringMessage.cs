using System;

namespace MyNetworking
{
    [Serializable]
    public class MyStringMessage : MyMessageBase
    {
        public string Value;

        public MyStringMessage()
        {

        }

        public MyStringMessage(string value)
        {
            Value = value;
        }
    }
}
