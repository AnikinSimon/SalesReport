using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class ITProductDto
    {
        public string Article { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Type { get; set; }

        // Дополнительные поля для наследников
        public int? RAM { get; set; }
        public string ProcessorType { get; set; }
        public double? ScreenSize { get; set; }
        public bool? Has5G { get; set; }
        public bool? HasPenSupport { get; set; }
        public int? StorageCapacity { get; set; }

        public ITProductDto() { }
    }
}
