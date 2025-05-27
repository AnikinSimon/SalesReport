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
    //[JsonDerivedType(typeof(Laptop), typeDiscriminator: "Laptop")]
    //[JsonDerivedType(typeof(Smartphone), typeDiscriminator: "Smartphone")]
    //[JsonDerivedType(typeof(Tablet), typeDiscriminator: "Tablet")]
    //[XmlInclude(typeof(Laptop))]
    //[XmlInclude(typeof(Smartphone))]
    //[XmlInclude(typeof(Tablet))]
    //public abstract class ITProduct
    //{
    //    public string Article { get; set; }
    //    public string Brand { get; set; }
    //    public string Model { get; set; }
    //    public DateTime? SaleDate { get; set; }
    //    public decimal BasePrice { get; set; }

    //    public virtual decimal Price => BasePrice;

    //    // Добавляем свойство Type для десериализации
    //    [JsonIgnore]
    //    [XmlIgnore]
    //    public virtual string Type => GetType().Name;
    //}

    [JsonConverter(typeof(PolymorphicConverter<ITProduct>))]
    public abstract class ITProduct
    {
        public string Article { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal BasePrice { get; set; }

        public virtual decimal Price => BasePrice;

        [JsonIgnore]
        public virtual string Type => GetType().Name;
    }
}
