using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Report : IReportable, IReportSelectable
    {
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public List<ITProduct> Devices { get; private set; }

        public Report(string name, DateTime start, DateTime end, List<ITProduct> devices)
        {
            Name = name;
            StartDate = start;
            EndDate = end;
            Devices =  new List<ITProduct>();
            AddDevices(devices);
        }

        public void Sort(bool ascending)
        {
            Devices = ascending ?
                Devices.OrderBy(d => d.Article).ToList() :
                Devices.OrderByDescending(d => d.Article).ToList();
        }

        public IEnumerable<ITProduct> Select(params Func<ITProduct, bool>[] selectParams)
        {
            IEnumerable<ITProduct> res = Devices;
            foreach (Func<ITProduct, bool> filter in selectParams)
            {
                res = res.Where(filter);
            }
            return res;
        }

        public IEnumerable<ITProduct> Select(Type deviceType)
        {
            return deviceType == typeof(ITProduct) ?
                Devices :
                Devices.Where(d => d.GetType() == deviceType);
        }

        public bool ContainsSalesInPeriod(DateTime date, string period)
        {
            DateTime otherDate = GetEndTime(date, period);
            return Devices.Any(d => d.SaleDate >= date && d.SaleDate <= otherDate);
        }

        public static DateTime GetEndTime(DateTime date, string period)
        {
            return period switch
            {
                "День" => date.AddDays(1),
                "Неделя" => date.AddDays(7),
                "Месяц" => date.AddMonths(1),
                "Квартал" => date.AddMonths(3),
                "Год" => date.AddYears(1),
                _ => date
            };
        }
    }

    public class ReportDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ITProductDto> Devices { get; set; }

        public ReportDto() { }

        public ReportDto(Report report) {
            Name = report.Name;
            StartDate = report.StartDate;
            EndDate = report.EndDate;
            Devices = report.Devices.Select(d => new ITProductDto(d)).ToList();
        }


    }
}
