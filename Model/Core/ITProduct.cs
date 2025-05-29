using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.Core
{
    public abstract class ITProduct
    {
        public Guid ID { get; private set; }
        public string Article { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public DateTime? SaleDate { get; private set; }
        public decimal BasePrice { get; private set; }


        public virtual decimal Price => BasePrice;

        [JsonIgnore]
        public virtual string Type => GetType().Name;

        public ITProduct(Guid id, string article, string brand, string model, decimal basePrice, DateTime? saleDate)
        {
            Article = article;
            Brand = brand;
            Model = model;
            BasePrice = basePrice;
            SaleDate = saleDate;
            ID = id;
        }

        //public ITProduct() { }
    }
}
