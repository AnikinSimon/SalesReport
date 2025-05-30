using Model.Core;
using Model.Data;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;

namespace SalesReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Report> _reports = new();
        private List<ReportChecking> _reportsInPeriod = new();
        // Приведение
        private ISerializer _serializer = new Model.Data.JsonSer();

        public MainWindow()
        {
            InitializeComponent();
            Trace.WriteLine($"INITIAL");
            LoadReports();
            SetupEventHandlers();
            dpReportDate.SelectedDate = DateTime.Today;
            
        }

        private void LoadReports()
        {
            try
            {
                string reportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "Reports");
                Trace.WriteLine(reportsPath);
                Directory.CreateDirectory(reportsPath);

                if (!Directory.GetFiles(reportsPath).Any())
                    GenerateSampleReports(reportsPath);


                foreach (var file in Directory.GetFiles(reportsPath))
                {
                    
                    try
                    {
                        var content = File.ReadAllText(file);

                        if (file.EndsWith(_serializer.Extension))
                        {
                            ReportDto dto = _serializer.Deserialize<ReportDto>(content);
                            _reports.Add(DtoManager.FromDto(dto));  
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Ошибка {ex.Message}");
                    }
                }

                lbAvailableReports.ItemsSource = _reportsInPeriod;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчетов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                Trace.WriteLine($"Ошибка при загрузке отчетов: {ex.Message}");
            }
        }

        private void GenerateSampleReports(string path)
        {
            Trace.WriteLine("GENERATED");
            var random = new Random();
            var devices = new List<ITProduct>();

            // Генерация тестовых устройств
            for (int i = 1; i <= 21; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    // Приведение
                    ITProduct device = (i % 3) switch
                    {
                        0 => new Laptop(Guid.NewGuid(), $"LP{i:000}", i % 2 == 0 ? "Asus" : "Lenovo", $"Model {i}", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 90)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]),
                        1 => new Smartphone(Guid.NewGuid(), $"SP{i:000}", i % 2 == 0 ? "Samsung" : "Apple", $"Galaxy {i}", 20000 + random.Next(0, 10) * 3000, DateTime.Today.AddDays(-random.Next(0, 90)), 5 + random.NextDouble() * 3, random.Next(0, 2) == 1),
                        _ => new Model.Core.Tablet(Guid.NewGuid(), $"TB{i:000}", i % 2 == 0 ? "Huawei" : "Apple", $"Tab {i}", 15000 + random.Next(0, 10) * 2000, DateTime.Today.AddDays(-random.Next(0, 90)), random.Next(0, 2) == 1, 32 * (1 + random.Next(0, 8)))
                    };
                    devices.Add(device);
                }
            }

            List<ITProduct> curMonth = new List<ITProduct>();
            List<ITProduct> lastMonth = new List<ITProduct>();
            List<ITProduct> curQuarter = new List<ITProduct> ();

            foreach(var device in devices)
            {
                if (device.SaleDate?.Month == DateTime.Today.Month)
                {
                    curMonth.Add(device);
                    //lastMonth.Add(device);
                } else if (device.SaleDate?.Month == DateTime.Today.AddMonths(-1).Month)
                {
                    lastMonth.Add(device);
                } else
                {
                    curQuarter.Add(device);
                }
            }

            var reports = new List<Report>
            {
                new("Отчет за текущий месяц",
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                    DateTime.Today, curMonth),
                new("Отчет за прошлый месяц",
                    new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1), lastMonth),
                new("Отчет за квартал",
                    new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1),
                    DateTime.Today, curQuarter)
            };

            Laptop newLaptop = new Laptop(Guid.NewGuid(), "LP101", "Lenovo", $"Model 554", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 30)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]);
            
            // Перегрузка оператора +, приведение
            reports[0] += newLaptop;
            reports[1] += newLaptop;
            reports[2].AddDevice(newLaptop);

            // Сохранение отчетов
            foreach (var report in reports)
            {

                foreach(var device in report.Devices)
                {
                    Trace.WriteLine(device);
                }

                // Обощенный тип
                string serialized = _serializer.Serialize<ReportDto>(DtoManager.ToDto(report));


                File.WriteAllText(System.IO.Path.Combine(path, $"{report.Name}{_serializer.Extension}"), serialized);
            }
        }

        private void SetupEventHandlers()
        {
            cbDeviceType.SelectionChanged += (s, e) => UpdateAvailableReports();
            cbReportPeriod.SelectionChanged += (s, e) => UpdateAvailableReports();
            dpReportDate.SelectedDateChanged += (s, e) => UpdateAvailableReports();

            btnShowReport.Click += (s, e) => ShowReport();

            cbSaveFormat.SelectionChanged += (s, e) => ChangeSerializationType();

        }

        private void ChangeSerializationType()
        {

            Trace.WriteLine("CHANGE TYPE");
            string newExt = cbSaveFormat.SelectedIndex == 0 ? ".json" : ".xml";
            string curExt = _serializer.Extension;
            if (curExt == newExt)
                return;

            // Приведение
            ISerializer tempSerializer = cbSaveFormat.SelectedIndex == 0 ?  new JsonSer() : new XmlSer();
            string reportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "Reports");
            Trace.WriteLine(curExt);

            foreach (string file in Directory.GetFiles(reportsPath))
            {
                Trace.WriteLine(file);
                var content = File.ReadAllText(file);
                  if (file.EndsWith(curExt))
                  {
                        Trace.WriteLine(file);
                        ReportDto report = _serializer.Deserialize<ReportDto>(content);
                        string fileName = $"{System.IO.Path.GetFileNameWithoutExtension(file)}{newExt}";
                        string filePath = System.IO.Path.Combine(reportsPath, fileName);
                        string serialized = tempSerializer.Serialize(report);
                        File.WriteAllText(filePath, serialized);
                  }
            }
            _serializer = tempSerializer;
        } 

        private void UpdateAvailableReports()
        {
            if (dpReportDate.SelectedDate == null) return;

            var period = (cbReportPeriod.SelectedItem as ComboBoxItem)?.Content.ToString();
            var deviceType = (cbDeviceType.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime startTime = dpReportDate.SelectedDate.Value;
            DateTime endtime = Report.GetEndTime(startTime, period);
            _reportsInPeriod.Clear();

            Trace.WriteLine(deviceType);

            foreach (Report report in _reports)
            {

                bool isSelected = report
                    .Select(ITProductExtensions.TypeByName(deviceType))
                    .Any(d => d.SaleDate >= startTime && d.SaleDate <= endtime);

                if (isSelected)
                {
                    ReportChecking ch = new ReportChecking(report);
                    ch.IsSelected = true;
                    _reportsInPeriod.Add(ch);
                }
                    
            }

            lbAvailableReports.Items.Refresh();
            btnShowReport.IsEnabled = _reportsInPeriod.Any(r => r.IsSelected);
        }

        private void ShowReport()
        {
            var selectedReports = _reportsInPeriod.Where(r => r.IsSelected).ToList();
            var deviceType = (cbDeviceType.SelectedItem as ComboBoxItem)?.Content.ToString();
            Type tp = ITProductExtensions.TypeByName(deviceType);

            var period = (cbReportPeriod.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime startTime = dpReportDate.SelectedDate.Value;

            DateTime endTime = Report.GetEndTime(startTime, period);
            var reportWindow = new ReportWindow(selectedReports.Select(r => r.BaseReport).ToList(), _serializer, tp, startTime, endTime);
            reportWindow.Show();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateButtonsState();
            e.Handled = true;
        }

        private void UpdateButtonsState()
        {
            bool anySelected = _reportsInPeriod?.Any(r => r.IsSelected) ?? false;
            btnShowReport.IsEnabled = anySelected;
        }


    }
}