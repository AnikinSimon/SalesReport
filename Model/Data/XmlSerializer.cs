using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class XmlSerializer : SerializerBase
    {
        // private static readonly System.Xml.Serialization.XmlSerializer _reportSerializer = 
        //new System.Xml.Serialization.XmlSerializer(typeof(Report), new Type[] { typeof(Laptop), typeof(Smartphone), typeof(Tablet) });

        private static readonly System.Xml.Serialization.XmlSerializer _reportSerializer = 
            new System.Xml.Serialization.XmlSerializer(typeof(ReportDto), new Type[] { typeof(ITProductDto) });

        public override string Serialize<T>(T obj)
        {
            using (var writer = new StringWriter())
            {
                _reportSerializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public override T Deserialize<T>(string data)
        {
            using (var reader = new StringReader(data))
            {
                return (T)_reportSerializer.Deserialize(reader);
            }
        }
    }
}
