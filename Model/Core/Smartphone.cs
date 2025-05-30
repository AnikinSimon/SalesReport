using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Smartphone : ITProduct
    {
        public double ScreenSize { get; private set; } // в дюймах
        public bool Has5G { get; private set; }

        public override decimal Price => BasePrice + (decimal)(ScreenSize * 3000) +
                                      (Has5G ? 8000 : 0);

        //public override decimal Price => BasePrice;


        public Smartphone(Guid id, string article, string brand, string model, decimal basePrice,
                        DateTime? saleDate, double screenSize, bool has5G)
            : base(id, article, brand, model, basePrice, saleDate)
        {
            ScreenSize = screenSize;
            Has5G = has5G;
        }

        public override string ToString()
        {
            return String.Join(" ", base.ToString(), $"ScreenSize: {ScreenSize}", $"Has5G: {Has5G}");
        }

        //public Smartphone() { }
    }
}
