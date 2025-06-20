﻿using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ISerializer _serializer;
        private readonly Type _goodsType;
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;

        public ReportWindow(List<Report> reports, ISerializer serializer, Type tp, DateTime startTime, DateTime endTime)

        {
            InitializeComponent();
            _reports = reports;
            _serializer = serializer;
            _goodsType = tp;
            _startTime = startTime;
            _endTime = endTime;

            LoadReportData();
            SetupEventHandlers();
        }

        private void LoadReportData()
        {

            // Объединяем устройства из всех отчетов
            Trace.WriteLine(_startTime);
            Trace.WriteLine(_endTime);

            var allDevices = _reports.SelectMany(r => r.Select(_goodsType))
                .Where(d => (d.SaleDate >= _startTime && d.SaleDate <= _endTime))
                .OrderBy(d => d.Article)
                .ToList();

            dgDevices.ItemsSource = allDevices;
            cbArticle.ItemsSource = allDevices.DistinctBy(g => g.Article);

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
            if (cbArticle.SelectedItem is ITProduct selectedDevice)
            {
                var selectedReports = _reports.ToList();
                var priceHistoryWindow = new PriceHistoryWindow(selectedReports, selectedDevice.Article, _startTime, _endTime);
                priceHistoryWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите устройство из списка", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
