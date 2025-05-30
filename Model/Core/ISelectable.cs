using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IReportSelectable
    {
        // делегат
        IEnumerable<ITProduct> Select(params Func<ITProduct, bool>[] selectParams);
    }
}
