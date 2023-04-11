using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyServer
{
    //[Serializable]
    //public abstract class MyMessageBase
    //{
    //    //public virtual void Serialize(TextWriter writer)
    //    //{

    //    //}

    //    //public virtual void Deserialize(TextReader reader)//streamreader instead
    //    //{

    //    //}

    //    //private void read<T>(byte[] data)
    //    //{
    //    //    ToObject<T>(data);
    //    //}

    //    public static T MakeAndReadMessage<T>(byte[] data) where T : MyMessageBase, new()
    //    {
    //        //var value = new T();
    //        //value.data = data;
    //        //value.read<T>(data);

    //        return ToObject<T>(data);
    //    }

    //    public static byte[] ToByteArray(object source)
    //    {
    //        var formatter = new BinaryFormatter();
    //        using (var stream = new MemoryStream())//can tap into this to use the Serialize/Deserialize above
    //        {
    //            formatter.Serialize(stream, source);
    //            return stream.ToArray();
    //        }
    //    }

    //    public static T ToObject<T>(byte[] rawData)
    //    {
    //        var formatter = new BinaryFormatter();
    //        using (var stream = new MemoryStream(rawData))
    //        {
    //            return (T)formatter.Deserialize(stream);
    //        }
    //    }
    //}
}
