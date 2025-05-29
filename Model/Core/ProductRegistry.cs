using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public static class ProductRegistry
    {
        private static readonly Dictionary<Guid, string> _productReports = new();
        public static bool TryRegisterProduct(ITProduct product, string reportName)
        {
            if (_productReports.ContainsKey(product.ID))
            {
                return _productReports[product.ID] == reportName;
            }
                
            _productReports.Add(product.ID, reportName);
            return true;
        }

        public static bool IsProductRegistered(Guid productId)
        {
             return _productReports.ContainsKey(productId);
        }
    }
}
