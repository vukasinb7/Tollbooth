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

        public ClerkWindow(TollStation station, ServiceBuilder serviceBuilder)
        {
            this.station = station;
            brushConverter = new();

            InitializeControllers(serviceBuilder);
            
            InitializeComponent();

            InitializeRamps();
            RampMalfunctionBtn.IsEnabled = false;
            RampText.IsReadOnly = true;

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
            foreach (int boothNum in station.TollBooths)
            {
                Device ramp = tollBoothController.FindBoothRamp(station.Id, boothNum);
                string functioning = "functioning";
                if (ramp.Malfunctioning)
                    functioning = "malfunctioning";
                string display = "Booth: " + boothNum + ", " + ramp.Name + " - " + functioning;
                RampStatusList.Items.Add(display);
                rampDisplay.Add(display, ramp);
            }
        }

        private void RampStatusList_SelectionChanged(object sender, 
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RampStatusList.SelectedIndex != -1)
            {
                Device ramp = rampDisplay[RampStatusList.SelectedItem.ToString()];
                RampText.Text = RampStatusList.SelectedItem.ToString();
                if (ramp.Malfunctioning)
                {
                    RampMalfunctionBtn.IsEnabled = false;
                    RampText.Background = (Brush)brushConverter.ConvertFrom("#ff0000");
                    return;
                }
                RampMalfunctionBtn.IsEnabled = true;
                RampText.Background = (Brush)brushConverter.ConvertFrom("#98fb98");
            }
        }

        private void RampMalfunctionBtn_Click(object sender, RoutedEventArgs e)
        {
            Device ramp = rampDisplay[RampStatusList.SelectedItem.ToString()];
            deviceController.ReportMalfunction(ramp.Id);
            RampText.Background = (Brush)brushConverter.ConvertFrom("#ff0000");

            MessageBox.Show("Malfunction reported.");
            InitializeRamps();
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
