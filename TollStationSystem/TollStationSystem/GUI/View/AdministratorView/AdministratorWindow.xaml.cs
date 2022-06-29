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
using TollStationSystem.Core.Locations.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controllers.Devices;
using TollStationSystem.GUI.Controllers.Locations;
using TollStationSystem.GUI.Controllers.PriceLists;
using TollStationSystem.GUI.Controllers.Sections;
using TollStationSystem.GUI.Controllers.TollBooths;
using TollStationSystem.GUI.Controllers.TollStations;
using TollStationSystem.GUI.Controllers.Users;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.GUI.View.AdministratorView
{
    
    public partial class AdministratorWindow : Window
    {
        ServiceBuilder serviceBuilder;
        SectionController sectionCotroller;
        TollStationController tollStationController;
        PriceListController priceListController;
        LocationController locationController;
        TollBoothController tollBoothController;
        BossController bossController;
        DeviceController deviceController;

        private Dictionary<int, TollStation> indexedTollStations;
        private Dictionary<int, TollBooth> indexedTollBooths;
        public AdministratorWindow()
        {
            InitializeComponent();
        }
        #region TollBooth
        private void InitializeTollBoothCb()
        {
            int index = 0;
            foreach (TollStation tollStation in tollStationController.TollStations)
            {
                Location location = locationController.FindByZip(tollStation.LocationZip);
                stationIdCb.Items.Add(tollStation.Id + "-" + location.Municipality);
                indexedTollStations.Add(index, tollStation);
                index++;
            }

            stationIdCb.SelectedIndex = 0;
        }

        private void InitilaizeTollBoothType()
        {
            TollBoothTypeCb.ItemsSource = Enum.GetValues(typeof(TollBoothType));
            TollBoothTypeCb.SelectedIndex = 0;
        }

        private void InitializeTollBoothLb()
        {
            tollBoothsLb.Items.Clear();
            indexedTollBooths.Clear();
            int index = 0;
            foreach (TollBooth tollBooth in tollBoothController.TollBooths)
            {
                string functioning = "In function";
                if (tollBooth.Malfunctioning)
                    functioning = "Not in function";
                tollBoothsLb.Items.Add(tollBooth.TollStationId.ToString() + "|" + tollBooth.Number.ToString() + "|" +
                                       tollBooth.TollBoothType.ToString().ToLower() + "|" +
                                       functioning);
                indexedTollBooths.Add(index, tollBooth);
                index++;
            }
        }

        private void createTollBothBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stationIdCb.SelectedIndex != -1 && TollBoothTypeCb.SelectedIndex != -1 && tollBoothNumberTb.Text != "")
                {
                    int number = Convert.ToInt32(tollBoothNumberTb.Text);
                    if (tollBoothController.AlreadyExist(indexedTollStations[stationIdCb.SelectedIndex].Id, number))
                        MessageBox.Show("Toll booth already exist!");
                    else
                    {
                        bool malfunctioning = malfunctioningTollBoothCh.IsChecked == true;
                        List<Device> devices = deviceController.GenerateDevices();
                        List<int> devicesId = new();
                        foreach (Device device in devices)
                            devicesId.Add(device.Id);
                        TollBoothDto tollBoothDto = new(indexedTollStations[stationIdCb.SelectedIndex].Id, number,
                            (TollBoothType)TollBoothTypeCb.SelectedItem, malfunctioning, devicesId);
                        tollBoothController.Add(tollBoothDto);
                        InitializeTollBoothLb();
                    }

                }
            }
            catch
            {
                MessageBox.Show("Number is not valid!");
            }

        }


        private void deselectBtn_Click(object sender, RoutedEventArgs e)
        {
            tollBoothsLb.SelectedIndex = -1;
        }

        private void tollBoothsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tollBoothsLb.SelectedIndex == -1)
            {
                stationIdCb.IsEnabled = true;
                createTollBoothBtn.IsEnabled = true;
                UpdateTollBoothBtn.IsEnabled = false;
                deleteTollBoothBtn.IsEnabled = false;
                tollBoothNumberTb.IsEnabled = true;
            }
            else
            {
                stationIdCb.IsEnabled = false;
                createTollBoothBtn.IsEnabled = false;
                UpdateTollBoothBtn.IsEnabled = true;
                deleteTollBoothBtn.IsEnabled = true;
                tollBoothNumberTb.IsEnabled = false;
                TollBooth tollBooth = indexedTollBooths[tollBoothsLb.SelectedIndex];
                TollStation tollStation = tollStationController.FindById(tollBooth.TollStationId);
                Location location = locationController.FindByZip(tollStation.LocationZip);
                stationIdCb.SelectedItem = tollBooth.TollStationId + "-" + location.Municipality;
                TollBoothTypeCb.SelectedItem = tollBooth.TollBoothType;
                tollBoothNumberTb.Text = tollBooth.Number.ToString();
                malfunctioningTollBoothCh.IsChecked = tollBooth.Malfunctioning;
            }
        }

        private void UpdateTollBoothBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stationIdCb.SelectedIndex != -1 && TollBoothTypeCb.SelectedIndex != -1 && tollBoothNumberTb.Text != "")
                {
                    int number = Convert.ToInt32(tollBoothNumberTb.Text);
                    bool malfunctioning = malfunctioningTollBoothCh.IsChecked == true;
                    List<Device> devices = deviceController.GenerateDevices();
                    List<int> devicesId = new();
                    foreach (Device device in devices)
                        devicesId.Add(device.Id);
                    TollBoothDto tollBoothDto = new(indexedTollStations[stationIdCb.SelectedIndex].Id, number,
                        (TollBoothType)TollBoothTypeCb.SelectedItem, malfunctioning, devicesId);
                    tollBoothController.Update(tollBoothDto);
                    InitializeTollBoothLb();

                }
            }
            catch
            {
                MessageBox.Show("Number is not valid!");
            }
        }

        private void deleteTollBoothBtn_Click(object sender, RoutedEventArgs e)
        {
            TollBooth tollBooth = indexedTollBooths[tollBoothsLb.SelectedIndex];
            tollBoothController.Delete(tollBooth.TollStationId, tollBooth.Number);
            InitializeTollBoothLb();
        }
        #endregion
    }
}
