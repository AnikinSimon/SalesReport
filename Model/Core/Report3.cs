using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Report
    {


        public List<PriceChange> GetPriceChanges(string article)
        {
            return Devices?
                .Where(d => d.Article == article && d.SaleDate.HasValue)
                .OrderBy(d => d.SaleDate)
                .Select(d => new PriceChange
                {
                    Date = d.SaleDate.Value,
                    Price = d.Price
                })
                .ToList() ?? new List<PriceChange>();
        }
    }

    public class PriceChange
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
