using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Sql_Business_Functions.Business.Helpers
{
    public static class ObjectToXmlHelper
    {
        public static T ToObject<T>(this string xml) where T : new()
        {
            using (var reader = new StringReader(xml))
            using (var xmlReader = new XmlTextReader(reader))
                return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }

        public static string ToXml<T>(this T obj)
        {
            using (var writer = new StringWriter())
            {
                new XmlSerializer(typeof(T)).Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}