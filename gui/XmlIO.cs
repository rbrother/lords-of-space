using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{
    public static class XmlIO
    {
        static public T LoadXml<T>(String fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        static public void SaveXml<T>(String fileName, T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        static public T CloneThroughXML<T>(this T data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, data);
            StringReader reader = new StringReader(writer.ToString());
            return (T) serializer.Deserialize(reader);
        }
    }
}
