using System;

namespace MyNetworking
{
    [Serializable]
    public class MyIntegerMessage : MyMessageBase
    {
        public int Value;

        public MyIntegerMessage()
        {

        }

        public MyIntegerMessage(int value)
        {
            Value = value;
        }
    }
}
