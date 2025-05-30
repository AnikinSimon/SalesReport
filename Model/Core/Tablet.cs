using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Tablet : ITProduct
    {
        public bool HasPenSupport { get; private set; }
        public int StorageCapacity { get; private set; } // в ГБ

        public override decimal Price => BasePrice +
                                      (HasPenSupport ? 5000 : 0) +
                                      (StorageCapacity * 200);


        //public override decimal Price => BasePrice;
        public Tablet(Guid id, string article, string brand, string model, decimal basePrice,
                      DateTime? saleDate, bool hasPenSupport, int storageCapacity)
             : base(id, article, brand, model, basePrice, saleDate)
        {
            HasPenSupport = hasPenSupport;
            StorageCapacity = storageCapacity;
        }

        public override string ToString()
        {
            return String.Join(" ",  base.ToString(), $"HasPenSupport: {HasPenSupport}", $"StorageCapacity: {StorageCapacity}");
        }

        //public Tablet() { }
    }
}
