using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public interface ISerializer
    {
        public string Serialize<T>(T obj) where T: class;
        public T Deserialize<T>(string data) where T : class;
    }
}
