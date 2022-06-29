using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controllers.Devices;
using TollStationSystem.GUI.Controllers.TollBooths;
using TollStationSystem.GUI.Controllers.TollStations;

namespace TollStationSystem.GUI.View.ClerkView
{
    public partial class ClerkWindow : Window
    {
        TollStationController tollStationController;
        DeviceController deviceController;
        TollBoothController tollBoothController;

        TollStation station;
        BrushConverter brushConverter;

        Dictionary<string, Device> rampDisplay;
        Dictionary<string, Device> deviceDisplay;

        public ClerkWindow(TollStation station, ServiceBuilder serviceBuilder)
        {
            this.station = station;
            brushConverter = new();

            InitializeControllers(serviceBuilder);
            
            InitializeComponent();

            InitializeRamps();
            InitializeRampBooths();
            RampMalfunctionBtn.IsEnabled = false;

            InitializeDevices();
            IntializeDeviceBooths();
            DeviceMalfunctionBtn.IsEnabled = false;
        }

        private void InitializeControllers(ServiceBuilder serviceBuilder)
        {
            tollStationController = new(serviceBuilder.TollStationService);
            deviceController = new(serviceBuilder.DeviceService);
            tollBoothController = new(serviceBuilder.TollBoothService);
        }

        private void InitializeRamps()
        {
            RampStatusList.Items.Clear();
            rampDisplay = new Dictionary<string, Device>();
            Dictionary<Device, int> ramps = tollStationController.FindRamps(station.Id);

            foreach (KeyValuePair<Device, int> rampData in ramps)
            {
                string functioning = "functioning";
                if (rampData.Key.Malfunctioning)
                    functioning = "malfunctioning";
                string display = "Booth: " + rampData.Value + ", " + rampData.Key.Name + " - " + functioning;
                RampStatusList.Items.Add(display);
                rampDisplay.Add(display, rampData.Key);
            }
        }

        private void InitializeRampBooths()
        {
            foreach (int boothNum in station.TollBooths)
                RampBoothComboBox.Items.Add(boothNum);
        }

        private void InitializeDevices()
        {
            DeviceStatusList.Items.Clear();
            deviceDisplay = new Dictionary<string, Device>();
            Dictionary<Device, int> devices = tollStationController.FindNonRampDevices(station.Id);

            foreach (KeyValuePair<Device, int> deviceData in devices)
            {
                string functioning = "functioning";
                if (deviceData.Key.Malfunctioning)
                    functioning = "malfunctioning";
                string display = "Booth: " + deviceData.Value + ", " + deviceData.Key.Name + " - " + functioning;
                DeviceStatusList.Items.Add(display);
                deviceDisplay.Add(display, deviceData.Key);
            }
        }

        private void IntializeDeviceBooths()
        {
            foreach (int boothNum in station.TollBooths)
                DeviceBoothComboBox.Items.Add(boothNum);
        }

        private void RampBoothComboBox_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int chosenBooth = (int)RampBoothComboBox.SelectedItem;

            RampStatusList.Items.Clear();
            rampDisplay = new Dictionary<string, Device>();

            Device ramp = tollBoothController.FindBoothRamp(station.Id, chosenBooth);
            string functioning = "functioning";
            if (ramp.Malfunctioning)
                functioning = "malfunctioning";

            string display = "Booth: " + chosenBooth + ", " + ramp.Name + " - " + functioning;
            RampStatusList.Items.Add(display);
            rampDisplay.Add(display, ramp);
            
        }

        private void ResetRampsBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeRamps();
        }

        private void RampStatusList_SelectionChanged(object sender, 
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RampStatusList.SelectedIndex != -1)
            {
                Device ramp = rampDisplay[RampStatusList.SelectedItem.ToString()];
                RampLabel.Content = RampStatusList.SelectedItem.ToString();
                if (ramp.Malfunctioning)
                {
                    RampMalfunctionBtn.IsEnabled = false;
                    RampLabel.Foreground = (Brush)brushConverter.ConvertFrom("#ff0000");
                    return;
                }
                RampMalfunctionBtn.IsEnabled = true;
                RampLabel.Foreground = (Brush)brushConverter.ConvertFrom("#98fb98");
            }
        }

        private void RampMalfunctionBtn_Click(object sender, RoutedEventArgs e)
        {
            Device ramp = rampDisplay[RampStatusList.SelectedItem.ToString()];
            deviceController.ReportMalfunction(ramp.Id);
            RampLabel.Foreground = (Brush)brushConverter.ConvertFrom("#ff0000");

            MessageBox.Show("Malfunction reported.");
            InitializeRamps();
        }

        private void DeviceBoothComboBox_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DeviceStatusList.Items.Clear();
            deviceDisplay = new Dictionary<string, Device>();
            int chosenBooth = (int)DeviceBoothComboBox.SelectedItem;
            List<Device> devices = tollBoothController.FindNonRampDevices(station.Id, chosenBooth);

            foreach (Device device in devices)
            {
                string functioning = "functioning";
                if (device.Malfunctioning)
                    functioning = "malfunctioning";

                string display = "Booth: " + chosenBooth + ", " + device.Name + " - " + functioning;
                DeviceStatusList.Items.Add(display);
                deviceDisplay.Add(display, device);
            }
        }

        private void ResetDevicesBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeDevices();
        }

        private void DeviceStatusList_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DeviceStatusList.SelectedIndex != -1)
            {
                Device device = deviceDisplay[DeviceStatusList.SelectedItem.ToString()];
                DeviceLabel.Content = DeviceStatusList.SelectedItem.ToString();
                if (device.Malfunctioning)
                {
                    DeviceMalfunctionBtn.IsEnabled = false;
                    DeviceLabel.Foreground = (Brush)brushConverter.ConvertFrom("#ff0000");
                    return;
                }
                DeviceMalfunctionBtn.IsEnabled = true;
                DeviceLabel.Foreground = (Brush)brushConverter.ConvertFrom("#00D100");
            }
        }

        private void DeviceMalfunctionBtn_Click(object sender, RoutedEventArgs e)
        {
            Device device = deviceDisplay[DeviceStatusList.SelectedItem.ToString()];
            deviceController.ReportMalfunction(device.Id);
            DeviceLabel.Foreground = (Brush)brushConverter.ConvertFrom("#ff0000");

            MessageBox.Show("Malfunction reported.");
            InitializeDevices();
        }

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
