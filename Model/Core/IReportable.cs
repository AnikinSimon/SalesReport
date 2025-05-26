using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IReportable
    {
        void Sort(bool ascending);
        IEnumerable<ITProduct> Select(Type deviceType);
    }
}
