using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Address_Book.Extentions
{
    public static class ObjectToXmlHelper
    {
        public static T ToObject<T>(this string xml)
        {
            using (var reader = new StringReader(xml))
            using (var xmlReader = new XmlTextReader(reader))
                return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }

        public static string ToXml<T>(this T obj, string encodingStyle = "utf-8")
        {
            using (var memory = new MemoryStream())
            using (var writer = XmlWriter.Create(memory, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                return Encoding.UTF8.GetString(memory.ToArray());
            }
        }
    }
}