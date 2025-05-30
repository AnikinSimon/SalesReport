using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class ReportChecking
    {
        public Report BaseReport { get; private set; }

        public bool IsSelected { get; set; }

        public ReportChecking(Report report) { 
            BaseReport = report;
        }
    }
}
