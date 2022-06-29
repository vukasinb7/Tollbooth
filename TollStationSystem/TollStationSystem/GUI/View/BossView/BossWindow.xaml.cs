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
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controllers.Devices;
using TollStationSystem.GUI.Controllers.Payments;
using TollStationSystem.GUI.Controllers.TollBooths;
using TollStationSystem.GUI.Controllers.TollStations;

namespace TollStationSystem.GUI.View.BossView
{
    
    public partial class BossWindow : Window
    {
        ServiceBuilder serviceBuilder;
        User boss;
        TollBoothController tollBoothController;
        TollStationController tollStationController;
        TollStation tollStation;
        DeviceController deviceController;
        PaymentController paymentController;

        public BossWindow(ServiceBuilder serviceBuilder, User boss)
        {
            this.serviceBuilder = serviceBuilder;
            this.boss = boss;
            InitializeComponent();
            InitializeControllers();
            tollStation = tollStationController.FindByBoss(boss.Jmbg);
            stationLbl.Content = tollStation.Name;
            InitializeTollBoothCb();
        }

        void InitializeControllers()
        {
            tollBoothController = new(serviceBuilder.TollBoothService);
            deviceController = new(serviceBuilder.DeviceService);
            tollStationController = new(serviceBuilder.TollStationService);
            paymentController = new(serviceBuilder.PaymentService);
        }

        #region TollBoothState

        void DisplayTollBoothData()
        {
            TollBooth tollBooth = tollBoothController.FindById(tollStation.Id, (int)tollBoothCb.SelectedItem);
            tollBoothTypeTb.Text = tollBooth.TollBoothType.ToString();
            if (tollBooth.Malfunctioning)
            {
                tollBoothStatusTb.Text = "Malfunctioning";
                enableStationBtn.IsEnabled = true;
            }
            else
            {
                tollBoothStatusTb.Text = "In Function";
                enableStationBtn.IsEnabled = false;
            }
        }

        void InitializeDevices()
        {
            deviceCb.Items.Clear();
            TollBooth tollBooth = tollBoothController.FindById(tollStation.Id, (int)tollBoothCb.SelectedItem);
            foreach (int deviceId in tollBooth.Devices)
            {
                deviceCb.Items.Add(deviceId);
            }
            deviceCb.SelectedIndex = 0;
        }

        void DisplayDeviceData()
        {
            if (deviceCb.SelectedIndex != -1)
            {
                Device device = deviceController.FindById((int)deviceCb.SelectedItem);
                deviceTypeTb.Text = device.DeviceType.ToString();
                if (device.Malfunctioning)
                {
                    fixDeviceBtn.IsEnabled = true;
                    deviceLbl.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    fixDeviceBtn.IsEnabled = false;
                    deviceLbl.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        void InitializeTollBoothCb()
        {
            List<TollBooth> tollBooths = tollBoothController.GetAllFromStation(tollStation);
            foreach (TollBooth tollBooth in tollBooths)
            {
                tollBoothCb.Items.Add(tollBooth.Number);
            }
            tollBoothCb.SelectedIndex = 0;
        }

        private void tollBoothCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayTollBoothData();
            InitializeDevices();

        }

        private void deviceCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayDeviceData();
        }

        private void fixDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
            deviceController.Fix(deviceController.FindById((int)deviceCb.SelectedItem));
            DisplayTollBoothData();
            DisplayDeviceData();
        }

        private void enableStationBtn_Click(object sender, RoutedEventArgs e)
        {
            tollBoothController.Fix(tollBoothController.FindById(tollStation.Id, (int)tollBoothCb.SelectedItem));
            DisplayTollBoothData();
            DisplayDeviceData();
        }


        #endregion

        #region Report
        private void SearchIncomeBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime from = fromIncomeDp.SelectedDate.Value;
            DateTime to = toIncomeDp.SelectedDate.Value;
            if (from > to)
            {
                MessageBox.Show("Invalid date interval");
                return;
            }

            Tuple<float, float> prices = paymentController.FindSumOfPayments(tollStation.Id, from, to);
            dinIncomeTb.Text = prices.Item1.ToString() + "RSD";
            eurIncomeTb.Text = prices.Item2.ToString() + "EUR";
        }


        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
            else e.Cancel = true;
        }

    }
}
