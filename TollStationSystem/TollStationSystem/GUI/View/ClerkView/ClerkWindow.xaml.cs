using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Payments.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controllers.Devices;
using TollStationSystem.GUI.Controllers.TollBooths;
using TollStationSystem.GUI.Controllers.TollStations;
using TollStationSystem.GUI.Controllers.Sections;
using TollStationSystem.GUI.Controllers.PriceLists;
using TollStationSystem.GUI.Controllers.SpeedingPenalties;
using TollStationSystem.GUI.Controllers.Payments;
using TollStationSystem.Core.Sections.Model;
using TollStationSystem.Core.PriceLists.Model;
using System;
using TollStationSystem.Core.SpeedingPenalties.Model;

namespace TollStationSystem.GUI.View.ClerkView
{
    public partial class ClerkWindow : Window
    {
        TollStationController tollStationController;
        DeviceController deviceController;
        TollBoothController tollBoothController;
        SectionController sectionCotroller;
        PriceListController priceListController;
        PaymentController paymentController;
        SpeedingPenaltyController speedingPenaltyController;

        TollStation station;
        BrushConverter brushConverter;
        Payment payment;
        float speed;

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

            InitializeStations();
        }

        private void InitializeControllers(ServiceBuilder serviceBuilder)
        {
            tollStationController = new(serviceBuilder.TollStationService);
            deviceController = new(serviceBuilder.DeviceService);
            tollBoothController = new(serviceBuilder.TollBoothService);
            sectionCotroller = new(serviceBuilder.SectionService);
            priceListController = new(serviceBuilder.PriceListService);
            paymentController = new(serviceBuilder.PaymentService);
            speedingPenaltyController = new(serviceBuilder.SpeedingPenaltyService);
            deviceController = new(serviceBuilder.DeviceService);
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

        private void InitializeStations()
        {
            ExitStationText.Text = station.Name;

            StationsComboBox.SelectedValuePath = "Key";
            StationsComboBox.DisplayMemberPath = "Value";
            foreach (TollStation tollStation in tollStationController.TollStations)
            {
                StationsComboBox.Items.Add(new KeyValuePair<int, string>(tollStation.Id, tollStation.Name));
            }

            foreach (string name in Enum.GetNames(typeof(VehicleType)))
            {
                VehiclesComboBox.Items.Add(name);
            }
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
                RampLabel.Foreground = (Brush)brushConverter.ConvertFrom("#00D100");
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

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StationsComboBox.SelectedIndex == -1 || VehiclesComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Enter all parametars");
                return;
            }

            try
            {
                int.Parse(EntranceHourBox.Text);
                int.Parse(EntranceMinBox.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Enter all parametars");
                return;
            }

            int exitId = station.Id;
            int entranceId = int.Parse(StationsComboBox.SelectedValue.ToString());
            VehicleType vehicleType = (VehicleType)VehiclesComboBox.SelectedIndex;
            
            Section section = sectionCotroller.GetSectionByStations(entranceId, exitId);
            if(section == null)
            {
                MessageBox.Show("Not existing section.");
                return;
            }
            
            Price price = priceListController.GetPriceBySectionId(section.Id, vehicleType);
            PriceText.Text = price.PriceDin.ToString();

            SetPayment(vehicleType, exitId, section);

            CheckSpeeding(section);

            ChangeText.Text = "";
        }

        private DateTime GetDate()
        {
            DateTime entraceDT = DateTime.Today;
            int hours = int.Parse(EntranceHourBox.Text);
            int minutes = int.Parse(EntranceMinBox.Text);
            entraceDT = entraceDT.AddHours(hours);
            entraceDT = entraceDT.AddMinutes(minutes);
            return entraceDT;
        }

        private string GetPlates()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 999);
            string plates = "SM" + num + "AA";
            PlatesText.Text = plates;

            return plates;
        }

        private void SetPayment(VehicleType vehicleType, int exitId, Section section)
        {
            DateTime entraceDT = GetDate();
            DateTime exitDT = DateTime.Now;
            string plates = GetPlates();
            int paymentId = paymentController.GenerateId();

            payment = new Payment(paymentId, entraceDT, exitDT, plates, vehicleType, exitId, 1, section.Id);

        }

        private void CheckSpeeding(Section section)
        {
            speed = paymentController.CheckSpeed(payment, section.Distance);
            if (speed < 0)
            {
                SpeedText.Text = "Acceptable";
                SpeedText.Background = (Brush)brushConverter.ConvertFrom("#98fb98");

            }
            else
            {
                SpeedText.Text = "Speeding";
                SpeedText.Background = (Brush)brushConverter.ConvertFrom("#ff0000");
                int penaltyId = speedingPenaltyController.GenerateId();
                SpeedingPenalty penalty = new SpeedingPenalty(penaltyId, payment.Id, payment.ExitDate, speed);
                speedingPenaltyController.Add(penalty);
            }

        }

        private void ChargeBtn_Click(object sender, RoutedEventArgs e)
        {
            paymentController.Add(payment);

            float price = float.Parse(PriceText.Text);
            float paid = float.Parse(PaidBox.Text);
            float change = paid - price;

            ChangeText.Text = change.ToString();

            ClearText();
        }

        private void ClearText()
        {
            EntranceHourBox.Text = "";
            EntranceMinBox.Text = "";
            SpeedText.Background = (Brush)brushConverter.ConvertFrom("#ffffff");
            SpeedText.Text = "";
            PlatesText.Text = "";
            PriceText.Text = "";
            PaidBox.Text = "";
        }

    }
}
