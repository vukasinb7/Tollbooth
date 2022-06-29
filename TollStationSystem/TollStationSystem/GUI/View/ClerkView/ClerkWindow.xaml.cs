using System.Collections.Generic;
using System.Windows;
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

        Dictionary<string, bool> rampStatusDisplay;
        Dictionary<string, Device> rampDisplay;

        public ClerkWindow(TollStation station, ServiceBuilder serviceBuilder)
        {
            this.station = station;

            InitializeControllers(serviceBuilder);
            
            InitializeComponent();
        }

        private void InitializeControllers(ServiceBuilder serviceBuilder)
        {
            tollStationController = new(serviceBuilder.TollStationService);
            deviceController = new(serviceBuilder.DeviceService);
            tollBoothController = new(serviceBuilder.TollBoothService);
        }

        private void ReportRampMalfunction_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
