using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class ITProduct
    {
        public string Article { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal BasePrice { get; set; }

        public virtual decimal Price => BasePrice;
        public virtual string Type => GetType().Name;

        public override string ToString()
        {
            return $"{Brand} {Model} ({Article}) - {Price:C}";
        }
    }
}
