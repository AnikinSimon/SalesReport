using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;
using Newtonsoft.Json;

namespace Model.Data
{
    public class JsonSer : SerializerBase
    {
        public override string Extension => ".json";
        public override T Deserialize<T>(string data)
        {
            T obj = JsonConvert.DeserializeObject<T>(data);
            return obj;
        }

        public override string Serialize<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }
    }
}
