using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesReport
{
    /// <summary>
    /// Interaction logic for PriceHistoryWindow.xaml
    /// </summary>
    public partial class PriceHistoryWindow : Window
    {
        //public SeriesCollection SeriesCollection { get; set; }
        //public Func<double, string> DateTimeFormatter { get; set; }

        //public PriceHistoryWindow(List<Report> reports, string article)
        //{
        //    InitializeComponent();
        //    Title = $"История цен - артикул {article}";

        //    BuildPriceChart(reports, article);
        //    DataContext = this;
        //}

        //private void BuildPriceChart(List<Report> reports, string article)
        //{
        //    // Получаем все продажи данного товара
        //    var sales = reports.SelectMany(r => r.Devices)
        //                     .Where(d => d.Article == article && d.SaleDate.HasValue)
        //                     .OrderBy(d => d.SaleDate)
        //                     .ToList();

        //    SeriesCollection = new SeriesCollection
        //    {
        //        new LineSeries
        //        {
        //            Title = "Цена продажи",
        //            Values = new ChartValues<decimal>(sales.Select(d => d.Price)),
        //            PointGeometry = DefaultGeometries.Circle,
        //            PointGeometrySize = 10
        //        }
        //    };

        //    DateTimeFormatter = value =>
        //        sales.ElementAt((int)value).SaleDate?.ToString("dd.MM.yyyy") ?? string.Empty;
        //}
    }
}
