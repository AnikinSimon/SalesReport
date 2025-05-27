using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Smartphone : ITProduct
    {
        public double ScreenSize { get; set; } // в дюймах
        public bool Has5G { get; set; }

        public override decimal Price => BasePrice + (decimal)(ScreenSize * 3000) +
                                      (Has5G ? 8000 : 0);

        //public override decimal Price => BasePrice;

        public Smartphone() { }
    }
}
