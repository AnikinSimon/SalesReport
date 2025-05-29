using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.Data
{
    public class XmlSer : SerializerBase
    {
        public override string Extension => ".xml";
        public override T Deserialize<T>(string data)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using var reader = new StringReader(data);
            return (T)serializer.Deserialize(reader);
        }

        public override string Serialize<T>(T obj)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using var writer = new StringWriter();
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }
}
