using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Tablet : ITProduct
    {
        public bool HasPenSupport { get; set; }
        public int StorageCapacity { get; set; } // в ГБ

        public override decimal Price => BasePrice +
                                      (HasPenSupport ? 5000 : 0) +
                                      (StorageCapacity * 200);
    }
}
