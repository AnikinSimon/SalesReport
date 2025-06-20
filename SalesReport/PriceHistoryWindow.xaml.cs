﻿using LiveCharts.Wpf;
using LiveCharts;
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
        public PriceHistoryWindow(List<Report> reports, string article, DateTime startTime, DateTime endTime)
        {
            InitializeComponent();

            // Находим устройство по артикулу
            var device = reports
                .SelectMany(r => r.Devices)
                .FirstOrDefault(d => d.Article == article);

            if (device == null)
            {
                MessageBox.Show("Устройство не найдено", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            // Устанавливаем информацию о товаре
            tbArticle.Text = device.Article;
            tbBrand.Text = device.Brand;
            tbModel.Text = device.Model;
            Title = $"История цен: {device.Brand} {device.Model} ({device.Article})";

            // Создаем ViewModel и устанавливаем DataContext
            var viewModel = new PriceHistoryViewModel(reports, article, startTime, endTime);
            DataContext = viewModel;
        }
    }

    public class PriceHistoryViewModel
    {
        public SeriesCollection SeriesCollection { get; private set; }
        public string[] DateLabels { get; private set; }
        public ChartValues<decimal> PriceValues { get; private set; }

        public PriceHistoryViewModel(List<Report> reports, string article, DateTime startTime, DateTime endTime)
        {

            // делегат
            var sales = reports
                .SelectMany(r => r.Select(
                    d => d.Article == article && d.SaleDate.HasValue,
                    d => d.SaleDate >= startTime && d.SaleDate <= endTime)
                )
                .GroupBy(d => d.SaleDate)
                .Select(g => new
                {
                    Date = g.Key,
                    AvgPrice = g.Average(d => d.Price),
                })
                .OrderBy(x => x.Date)
                .ToList();

            // Подготавливаем данные для графика
            PriceValues = new ChartValues<decimal>(sales.Select(d => d.AvgPrice));
            DateLabels = sales.Select(d => d.Date?.ToString("dd.MM.yyyy")).ToArray();

            // Настройка серий
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Цена продажи",
                    Values = PriceValues,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                    StrokeThickness = 2
                }
            };
        }
    }
}
