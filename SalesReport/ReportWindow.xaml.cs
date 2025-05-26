using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private readonly List<Report> _reports;
        private readonly SerializerBase _serializer;

        public ReportWindow(List<Report> reports, SerializerBase serializer)
        {
            InitializeComponent();
            _reports = reports;
            _serializer = serializer;

            LoadReportData();
            SetupEventHandlers();
        }

        private void LoadReportData()
        {
            // Объединяем устройства из всех отчетов
            var allDevices = _reports.SelectMany(r => r.Devices)
                                   .DistinctBy(d => d.Article)
                                   .OrderBy(d => d.Article)
                                   .ToList();

            dgDevices.ItemsSource = allDevices;
            cbArticle.ItemsSource = allDevices;

            // Устанавливаем заголовок
            tbReportTitle.Text = _reports.Count == 1
                ? _reports[0].Name
                : $"Сводный отчет ({_reports.Count} отчетов)";
        }

        private void SetupEventHandlers()
        {
            rbSortAsc.Checked += (s, e) => SortDevices(true);
            rbSortDesc.Checked += (s, e) => SortDevices(false);
            btnShowPriceHistory.Click += (s, e) => ShowPriceHistory();
        }

        private void SortDevices(bool ascending)
        {
            var devices = dgDevices.ItemsSource as IEnumerable<ITProduct>;
            dgDevices.ItemsSource = ascending
                ? devices.OrderBy(d => d.Article)
                : devices.OrderByDescending(d => d.Article);
        }

        private void ShowPriceHistory()
        {
            //if (cbArticle.SelectedItem is ITProduct selectedDevice)
            //{
            //    var priceHistoryWindow = new PriceHistoryWindow(_reports, selectedDevice.Article);
            //    priceHistoryWindow.Show();
            //}
        }

        private void SaveReport()
        {
            try
            {
                string reportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "Reports");
                string fileName = $"Отчет_{DateTime.Now:yyyyMMdd_HHmmss}.{(_serializer is JsonSerializer ? "json" : "xml")}";
                string filePath = System.IO.Path.Combine(reportsPath, fileName);

                var combinedReport = new Report(_reports, _reports.Min(r => r.StartDate), _reports.Max(r => r.EndDate));
                string serialized = _serializer.Serialize(combinedReport);

                File.WriteAllText(filePath, serialized);
                MessageBox.Show("Отчет успешно сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
