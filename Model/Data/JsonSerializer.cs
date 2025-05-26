using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Model.Data
{
    public class JsonSerializer : SerializerBase
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
        };

        public override string Serialize<T>(T obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, _options);
        }

        public override T Deserialize<T>(string data)
        {


            return System.Text.Json.JsonSerializer.Deserialize<T>(data);
        }
    }
}
