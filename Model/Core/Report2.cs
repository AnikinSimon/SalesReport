using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Report
    {
        public Report(List<Report> reports, DateTime startDate, DateTime endDate)
        {
            Name = $"Сводный отчет за {startDate:d} - {endDate:d}";
            StartDate = startDate;
            EndDate = endDate;

            Devices = reports
                .SelectMany(r => r.Devices)
                .Where(d => d.SaleDate >= startDate && d.SaleDate <= endDate)
                .DistinctBy(d => d.Article)
                .ToList();
        }

        public void AddDevice(ITProduct device)
        {
            if (device == null)
                return;
            if (ProductRegistry.TryRegisterProduct(device, this.Name) && !Devices.Contains(device))
            {
                Trace.WriteLine($"ADDED {device.ID}");
                Devices.Add(device);
            } else
            {
                Trace.WriteLine("ALREADY REGISTERED");
            }
                
        }

        public void AddDevices(IEnumerable<ITProduct> devices)
        {
            foreach (var device in devices)
                AddDevice(device);
        }

        public void MergeReport(Report report)
        {
            AddDevices(report.Devices);
        }

        public static Report operator +(Report report, ITProduct device)
        {
            Report res = new Report(report.Name, report.StartDate, report.EndDate, report.Devices);
            res.AddDevice(device);

            return res;
        }
    }
}
