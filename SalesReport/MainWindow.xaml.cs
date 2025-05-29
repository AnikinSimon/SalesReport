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

namespace SalesReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Report> _reports = new();
        private List<Report> _reportsInPeriod = new();
        private SerializerBase _currentSerializer = new Model.Data.JsonSerializer();
        private ISerializer _serializer = new Model.Data.JsonSer();

        public MainWindow()
        {
            bool isJson = true;
            InitializeComponent();
            Trace.WriteLine($"HElloe");
            if (!isJson)
                _serializer = new Model.Data.XmlSer();
            LoadReports(isJson);
            SetupEventHandlers();
            dpReportDate.SelectedDate = DateTime.Today;
            
        }

        private void LoadReports(bool isJson = true)
        {
            try
            {
                string reportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "Reports");
                Trace.WriteLine(reportsPath);
                Directory.CreateDirectory(reportsPath);

                if (!Directory.GetFiles(reportsPath).Any())
                    GenerateSampleReports(reportsPath, isJson);


                foreach (var file in Directory.GetFiles(reportsPath))
                {
                    
                    try
                    {
                        var content = File.ReadAllText(file);
                        if (isJson)
                        {
                            
                            if (file.EndsWith(".json"))
                            {
                                Trace.WriteLine(file);
                                //ReportDto report = new Model.Data.JsonSerializer().Deserialize<ReportDto>(content);
                                ReportDto report = _serializer.Deserialize<ReportDto>(content);
                                _reports.Add(Report.FromDto(report));
                            }
                        } else
                        {
                            if (file.EndsWith(".xml"))
                            {
                                Trace.WriteLine(file);
                                ReportDto report = _serializer.Deserialize<ReportDto>(content);
                                Trace.WriteLine(report);
                                _reports.Add(Report.FromDto(report));
                            }
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Failed to load {file}");
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

        private void GenerateSampleReports(string path, bool isJson = true)
        {
            Trace.WriteLine("GENERATED");
            var random = new Random();
            var devices = new List<ITProduct>();

            // Генерация тестовых устройств
            for (int i = 1; i <= 20; i++)
            {
                ITProduct device = (i % 3) switch
                {
                    0 => new Laptop($"LP{i:000}", i % 2 == 0 ? "Asus" : "Lenovo", $"Model {i}", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 90)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]),
                    1 => new Smartphone ($"SP{i:000}", i % 2 == 0 ? "Samsung" : "Apple", $"Galaxy {i}", 20000 + random.Next(0, 10) * 3000, DateTime.Today.AddDays(-random.Next(0, 90)), 5 + random.NextDouble() * 3, random.Next(0, 2) == 1),
                    _ => new Model.Core.Tablet($"TB{i:000}", i % 2 == 0 ? "Huawei" : "Apple", $"Tab {i}", 15000 + random.Next(0, 10) * 2000, DateTime.Today.AddDays(-random.Next(0, 90)),  random.Next(0, 2) == 1, 32 * (1 + random.Next(0, 8)))
                };

                ITProduct device2 = (i % 3) switch
                {
                    0 => new Laptop($"LP{i:000}", i % 2 == 0 ? "Asus" : "Lenovo", $"Model {i}", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 90)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]),
                    1 => new Smartphone($"SP{i:000}", i % 2 == 0 ? "Samsung" : "Apple", $"Galaxy {i}", 20000 + random.Next(0, 10) * 3000, DateTime.Today.AddDays(-random.Next(0, 90)), 5 + random.NextDouble() * 3, random.Next(0, 2) == 1),
                    _ => new Model.Core.Tablet($"TB{i:000}", i % 2 == 0 ? "Huawei" : "Apple", $"Tab {i}", 15000 + random.Next(0, 10) * 2000, DateTime.Today.AddDays(-random.Next(0, 90)), random.Next(0, 2) == 1, 32 * (1 + random.Next(0, 8)))
                };

                ITProduct device3 = (i % 3) switch
                {
                    0 => new Laptop($"LP{i:000}", i % 2 == 0 ? "Asus" : "Lenovo", $"Model {i}", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 90)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]),
                    1 => new Smartphone($"SP{i:000}", i % 2 == 0 ? "Samsung" : "Apple", $"Galaxy {i}", 20000 + random.Next(0, 10) * 3000, DateTime.Today.AddDays(-random.Next(0, 90)), 5 + random.NextDouble() * 3, random.Next(0, 2) == 1),
                    _ => new Model.Core.Tablet($"TB{i:000}", i % 2 == 0 ? "Huawei" : "Apple", $"Tab {i}", 15000 + random.Next(0, 10) * 2000, DateTime.Today.AddDays(-random.Next(0, 90)), random.Next(0, 2) == 1, 32 * (1 + random.Next(0, 8)))
                };

                devices.Add(device);
                devices.Add(device2);
                devices.Add(device3);
            }

            List<ITProduct> curMonth = new List<ITProduct>();
            List<ITProduct> lastMonth = new List<ITProduct>();
            List<ITProduct> curQuarter = new List<ITProduct> ();

            foreach(var device in devices)
            {
                if (device.SaleDate?.Month == DateTime.Today.Month)
                {
                    curMonth.Add(device);
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

            // Создание тестовых отчетов
            //var reports = new List<Report>
            //{
            //    new("Отчет за текущий месяц",
            //        new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
            //        DateTime.Today,
            //        devices.Where(d => d.SaleDate?.Month == DateTime.Today.Month).ToList()),
            //    new("Отчет за прошлый месяц",
            //        new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
            //        new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1),
            //        devices.Where(d => d.SaleDate?.Month == DateTime.Today.AddMonths(-1).Month).ToList()),
            //    new("Отчет за квартал",
            //        new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1),
            //        DateTime.Today,
            //        devices.Where(d => d.SaleDate >= new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1)).ToList())
            //};

            Laptop newLaptop = new Laptop("LP101", "Lenovo", $"Model 554", 30000 + random.Next(0, 10) * 5000, DateTime.Today.AddDays(-random.Next(0, 30)), 4 * (1 + random.Next(0, 4)), new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)]);
            reports[0] += newLaptop;

            // Сохранение отчетов
            foreach (var report in reports)
            {
                if (isJson)
                {
                    //Trace.WriteLine(report);
                    //var json = new Model.Data.JsonSerializer().Serialize(report);
                    string json = _serializer.Serialize<ReportDto>(report.ToDto());
                    //Trace.WriteLine(json);
                    File.WriteAllText(System.IO.Path.Combine(path, $"{report.Name}.json"), json);
                }
                else
                {
                    try
                    {
                        var dto = report.ToDto();
                        Trace.WriteLine(dto.GetType().Name);
                        var xm = new Model.Data.XmlSerializer().Serialize(dto);
                        File.WriteAllText(System.IO.Path.Combine(path, $"{report.Name}.xml"), xm);
                    } catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                    }
                    
                }
                
            }
        }

        private void SetupEventHandlers()
        {
            cbDeviceType.SelectionChanged += (s, e) => UpdateAvailableReports();
            cbReportPeriod.SelectionChanged += (s, e) => UpdateAvailableReports();
            dpReportDate.SelectedDateChanged += (s, e) => UpdateAvailableReports();

            btnShowReport.Click += (s, e) => ShowReport();
            btnPriceHistory.Click += (s, e) => ShowPriceHistory();

            cbSaveFormat.SelectionChanged += (s, e) => ChangeSerializationType();


        }

        private void ChangeSerializationType()
        {

            Trace.WriteLine("CHANGE TYPE");
            string newExt = cbSaveFormat.SelectedIndex == 0 ? ".json" : ".xml";
            string curExt = cbSaveFormat.SelectedIndex == 0 ? ".xml" : ".json";
            SerializerBase tempSerializer = cbSaveFormat.SelectedIndex == 0 ?  new JsonSer() : new XmlSer();
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

            _reportsInPeriod.Clear();

            foreach (Report report in _reports)
            {
                //if (report is null) continue;
                report.IsSelected = report.ContainsSalesInPeriod(dpReportDate.SelectedDate.Value, period) &&
                    (deviceType == "Все устройства" ||
                     report.Devices.Any(d => deviceType switch
                     {
                         "Ноутбуки" => d is Laptop,
                         "Смартфоны" => d is Smartphone,
                         "Планшеты" => d is Model.Core.Tablet,
                         _ => true
                     }));
                if (report.IsSelected)
                    _reportsInPeriod.Add(report);
            }

            lbAvailableReports.Items.Refresh();
            btnShowReport.IsEnabled = _reportsInPeriod.Any(r => r.IsSelected);
            btnPriceHistory.IsEnabled = _reportsInPeriod.Any(r => r.IsSelected);
        }

        private void ShowReport()
        {
            var selectedReports = _reportsInPeriod.Where(r => r.IsSelected).ToList();
            var deviceType = (cbDeviceType.SelectedItem as ComboBoxItem)?.Content.ToString();
            Type tp = deviceType switch
            {
                "Ноутбуки" => typeof(Laptop),
                "Смартфоны" => typeof(Smartphone),
                "Планшеты" => typeof(Model.Core.Tablet),
                _ => typeof(ITProduct),
            };
            var period = (cbReportPeriod.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime startTime = dpReportDate.SelectedDate.Value;
            DateTime endTime = period switch
            {
                "День" => startTime.AddDays(1),
                "Неделя" => startTime.AddDays(7),
                "Месяц" => startTime.AddMonths(1),
                "Квартал" => startTime.AddMonths(3),
                "Год" => startTime.AddYears(1),
                _ => startTime
            };
            var reportWindow = new ReportWindow(selectedReports, _serializer, tp, startTime, endTime);
            reportWindow.Show();
        }

        private void ShowPriceHistory()
        {
            //var selectedReports = _reports.Where(r => r.IsSelected).ToList();
            //var priceHistoryWindow = new PriceHistoryWindow(selectedReports, "art1");
            //priceHistoryWindow.Show();
        }
    }
}