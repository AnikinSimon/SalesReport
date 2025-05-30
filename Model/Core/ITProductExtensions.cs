using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public static class ITProductExtensions
    { 

        public static Type TypeByName(string name)
        {
            return name switch
            {
                "Ноутбуки" => typeof(Laptop),
                "Смартфоны" => typeof(Smartphone),
                "Планшеты" => typeof(Model.Core.Tablet),
                _ => typeof(ITProduct),
            };
        }
    }
}
