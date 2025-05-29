using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class SerializerBase: ISerializer
    {
        public abstract string Serialize<T>(T obj) where T: class;
        public abstract T Deserialize<T>(string data) where T: class;

        public abstract string Extension { get; }
    }
}
