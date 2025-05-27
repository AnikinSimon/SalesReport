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
        private SerializerBase _currentSerializer = new Model.Data.JsonSerializer();

        public MainWindow()
        {
            InitializeComponent();
            Trace.WriteLine($"HElloe");
            LoadReports(false);
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
                                Report report = new Model.Data.JsonSerializer().Deserialize<Report>(content);
                                _reports.Add(report);
                            }
                        } else
                        {
                            if (file.EndsWith(".xml"))
                            {
                                Trace.WriteLine(file);
                                Report report = new Model.Data.XmlSerializer().Deserialize<Report>(content);
                                _reports.Add(report);
                            }
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Failed to load {file}");
                        Trace.WriteLine($"Ошибка {ex.Message}");
                    }
                }

                lbAvailableReports.ItemsSource = _reports;
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
            var random = new Random();
            var devices = new List<ITProduct>();

            // Генерация тестовых устройств
            for (int i = 1; i <= 20; i++)
            {
                ITProduct device = (i % 3) switch
                {
                    0 => new Laptop
                    {
                        Article = $"LP{i:000}",
                        Brand = i % 2 == 0 ? "Asus" : "Lenovo",
                        Model = $"Model {i}",
                        BasePrice = 30000 + random.Next(0, 10) * 5000,
                        RAM = 4 * (1 + random.Next(0, 4)),
                        ProcessorType = new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)],
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    1 => new Smartphone
                    {
                        Article = $"SP{i:000}",
                        Brand = i % 2 == 0 ? "Samsung" : "Apple",
                        Model = $"Galaxy {i}",
                        BasePrice = 20000 + random.Next(0, 10) * 3000,
                        ScreenSize = 5 + random.NextDouble() * 3,
                        Has5G = random.Next(0, 2) == 1,
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    _ => new Model.Core.Tablet
                    {
                        Article = $"TB{i:000}",
                        Brand = i % 2 == 0 ? "Huawei" : "Apple",
                        Model = $"Tab {i}",
                        BasePrice = 15000 + random.Next(0, 10) * 2000,
                        HasPenSupport = random.Next(0, 2) == 1,
                        StorageCapacity = 32 * (1 + random.Next(0, 8)),
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    }
                };
                ITProduct device2 = (i % 3) switch
                {
                    0 => new Laptop
                    {
                        Article = $"LP{i:000}",
                        Brand = i % 2 == 0 ? "Asus" : "Lenovo",
                        Model = $"Model {i}",
                        BasePrice = 30000 + random.Next(0, 10) * 5000,
                        RAM = 4 * (1 + random.Next(0, 4)),
                        ProcessorType = new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)],
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    1 => new Smartphone
                    {
                        Article = $"SP{i:000}",
                        Brand = i % 2 == 0 ? "Samsung" : "Apple",
                        Model = $"Galaxy {i}",
                        BasePrice = 20000 + random.Next(0, 10) * 3000,
                        ScreenSize = 5 + random.NextDouble() * 3,
                        Has5G = random.Next(0, 2) == 1,
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    _ => new Model.Core.Tablet
                    {
                        Article = $"TB{i:000}",
                        Brand = i % 2 == 0 ? "Huawei" : "Apple",
                        Model = $"Tab {i}",
                        BasePrice = 15000 + random.Next(0, 10) * 2000,
                        HasPenSupport = random.Next(0, 2) == 1,
                        StorageCapacity = 32 * (1 + random.Next(0, 8)),
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    }
                };

                ITProduct device3 = (i % 3) switch
                {
                    0 => new Laptop
                    {
                        Article = $"LP{i:000}",
                        Brand = i % 2 == 0 ? "Asus" : "Lenovo",
                        Model = $"Model {i}",
                        BasePrice = 30000 + random.Next(0, 10) * 5000,
                        RAM = 4 * (1 + random.Next(0, 4)),
                        ProcessorType = new[] { "i3", "i5", "i7", "i9" }[random.Next(0, 4)],
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    1 => new Smartphone
                    {
                        Article = $"SP{i:000}",
                        Brand = i % 2 == 0 ? "Samsung" : "Apple",
                        Model = $"Galaxy {i}",
                        BasePrice = 20000 + random.Next(0, 10) * 3000,
                        ScreenSize = 5 + random.NextDouble() * 3,
                        Has5G = random.Next(0, 2) == 1,
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    },
                    _ => new Model.Core.Tablet
                    {
                        Article = $"TB{i:000}",
                        Brand = i % 2 == 0 ? "Huawei" : "Apple",
                        Model = $"Tab {i}",
                        BasePrice = 15000 + random.Next(0, 10) * 2000,
                        HasPenSupport = random.Next(0, 2) == 1,
                        StorageCapacity = 32 * (1 + random.Next(0, 8)),
                        SaleDate = DateTime.Today.AddDays(-random.Next(0, 30))
                    }
                };
                devices.Add(device);
                devices.Add(device2);
                devices.Add(device3);
            }



            // Создание тестовых отчетов
            var reports = new List<Report>
            {
                new("Отчет за текущий месяц",
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                    DateTime.Today,
                    devices.Where(d => d.SaleDate?.Month == DateTime.Today.Month).ToList()),
                new("Отчет за прошлый месяц",
                    new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1),
                    devices.Where(d => d.SaleDate?.Month == DateTime.Today.AddMonths(-1).Month).ToList()),
                new("Отчет за квартал",
                    new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1),
                    DateTime.Today,
                    devices.Where(d => d.SaleDate >= new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1)).ToList())
            };

            // Сохранение отчетов
            foreach (var report in reports)
            {
                if (isJson)
                {
                    //Trace.WriteLine(report);
                    var json = new Model.Data.JsonSerializer().Serialize(report);
                    //Trace.WriteLine(json);
                    File.WriteAllText(System.IO.Path.Combine(path, $"{report.Name}.json"), json);
                } else
                {
                    var xm = new Model.Data.XmlSerializer().Serialize(report);
                    File.WriteAllText(System.IO.Path.Combine(path, $"{report.Name}.xml"), xm);
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

            cbSaveFormat.SelectionChanged += (s, e) =>
                _currentSerializer = cbSaveFormat.SelectedIndex == 0 ?
                    new Model.Data.JsonSerializer() :
                    new Model.Data.XmlSerializer();
        }

        private void UpdateAvailableReports()
        {
            if (dpReportDate.SelectedDate == null) return;

            var period = (cbReportPeriod.SelectedItem as ComboBoxItem)?.Content.ToString();
            var deviceType = (cbDeviceType.SelectedItem as ComboBoxItem)?.Content.ToString();

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
            }

            lbAvailableReports.Items.Refresh();
            btnShowReport.IsEnabled = _reports.Any(r => r.IsSelected);
            btnPriceHistory.IsEnabled = _reports.Any(r => r.IsSelected);
        }

        private void ShowReport()
        {
            var selectedReports = _reports.Where(r => r.IsSelected).ToList();
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
            var reportWindow = new ReportWindow(selectedReports, _currentSerializer, tp, startTime, endTime);
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