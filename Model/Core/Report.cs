using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Report : IReportable
    {
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public List<ITProduct> Devices { get; private set; }
        public bool IsSelected { get; set; }

        public Report() { }

        public Report(string name, DateTime start, DateTime end, List<ITProduct> devices)
        {
            Name = name;
            StartDate = start;
            EndDate = end;
            Devices = devices ?? new List<ITProduct>();
        }

        public void Sort(bool ascending)
        {
            Devices = ascending ?
                Devices.OrderBy(d => d.Article).ToList() :
                Devices.OrderByDescending(d => d.Article).ToList();
        }

        public IEnumerable<ITProduct> Select(Type deviceType)
        {
            return deviceType == typeof(ITProduct) ?
                Devices :
                Devices.Where(d => d.GetType() == deviceType);
        }

        public bool ContainsSalesInPeriod(DateTime date, string period)
        {
            return period switch
            {
                "День" => Devices.Any(d => d.SaleDate?.Date == date.Date),
                "Неделя" => Devices.Any(d => d.SaleDate >= date && d.SaleDate < date.AddDays(7)),
                "Месяц" => Devices.Any(d => d.SaleDate >= date && d.SaleDate < date.AddMonths(1)),
                "Квартал" => Devices.Any(d => d.SaleDate >= date && d.SaleDate < date.AddMonths(3)),
                "Год" => Devices.Any(d => d.SaleDate >= date && d.SaleDate < date.AddYears(1)),
                _ => false
            };
        }
    }

    public partial class Report
    {

        // Фабричный метод для создания отчета
        public static Report Create(string name, DateTime start, DateTime end, List<ITProduct> devices)
        {
            return new Report(name, start, end, devices);
        }

        // Метод для сериализации
        public ReportDto ToDto()
        {
            return new ReportDto
            {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate,
                Devices = Devices.Select(d => d.ToDto()).ToList()
            };
        }

        // Метод для десериализации
        public static Report FromDto(ReportDto dto)
        {
            return new Report(
                dto.Name,
                dto.StartDate,
                dto.EndDate,
                dto.Devices.Select(ITProductExtensions.FromDto).ToList());
        }
    }

    public class ReportDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ITProductDto> Devices { get; set; }

        public ReportDto() { }  
    }
}
