﻿using System;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RollingMarbleSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Accelerometer _accelerometer;
        private double _minX = 0;
        private double _minY = 0;
        private double _maxX = 1000;
        private double _maxY = 600;
        private double _currentX = 0;
        private double _currentY = 0;

        public MainPage()
        {
            this.InitializeComponent();
            _accelerometer = Accelerometer.GetDefault();
           
            _accelerometer.ReportInterval = _accelerometer.MinimumReportInterval;
            _accelerometer.ReadingChanged += OnAccelerometerReading;

            LayoutUpdated += (sender, e) =>
            {
                _maxX = ActualWidth - 100;
                _maxY = ActualHeight - 100;
            };

        }

        private async void OnAccelerometerReading(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            _currentX += args.Reading.AccelerationX * 80;
            if (_currentX < _minX) _currentX = _minX;
            if (_currentX > _maxX) _currentX = _maxX;

            _currentY += -args.Reading.AccelerationY * 80;
            if (_currentY < _minY) _currentY = _minY;
            if (_currentY > _maxY) _currentY = _maxY;

            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                Canvas.SetLeft(ell1, _currentX);
                Canvas.SetTop(ell1, _currentY);
            });
        }

    }
}
