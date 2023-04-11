using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MyServer
{
    //public delegate void MyNetworkMessageDelegate(MyNetworkMessage netMsg);

    //public class MyNetworkMessage : IDisposable
    //{
    //    private MemoryStream dataStream;

    //    public Socket Connection { get; private set; }
    //    public short MsgType { get; private set; }
    //    public bool IsDisposed { get; private set; }

    //    public MyNetworkMessage(Socket socket, byte[] data)
    //    {
    //        Connection = socket;
    //        //dataStream = new MemoryStream(data);

    //        using (MemoryStream mem = new MemoryStream(data))
    //        {
    //            using (BinaryReader reader = new BinaryReader(mem))
    //            {
    //                MsgType = reader.ReadInt16();

    //                //byte[] remaining = reader.ReadBytes(data.Length - sizeof(short));
    //                dataStream = new MemoryStream(reader.ReadBytes(data.Length - sizeof(short)));
    //            }
    //        }
    //    }

    //    ~MyNetworkMessage()
    //    {
    //        Dispose();
    //    }

    //    public T ReadMessage<T>() where T : MyMessageBase, new()
    //    {
    //        //byte[] buffer = new byte[dataStream.Length - dataStream.Position];
    //        //Array.Copy(dataStream.ToArray(), (int)dataStream.Position, buffer, 0, buffer.Length);
    //        //Dispose();

    //        var result = MyMessageBase.MakeAndReadMessage<T>(dataStream.ToArray());
    //        Dispose();

    //        return result;
    //    }

    //    public void Dispose()
    //    {
    //        if (IsDisposed)
    //            return;

    //        dataStream.Close();
    //        dataStream.Dispose();
    //        IsDisposed = true;
    //    }
    //}
}
