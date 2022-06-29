﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.Locations.Model;
using TollStationSystem.Core.PriceLists.Model;
using TollStationSystem.Core.Sections.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Users.Model;
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

        public AdministratorWindow(ServiceBuilder serviceBuilder)
        {
            this.serviceBuilder = serviceBuilder;
            indexedTollStations = new();
            indexedTollBooths = new();
            InitializeComponent();
            InitializeControllers();
            InitializeCb();
            InitializeTollBoothCb();
            InitilaizeTollBoothType();
            InitializeTollBoothLb();
            InitializeTollStationsCb();


            UpdateTollBoothBtn.IsEnabled = false;
            deleteTollBoothBtn.IsEnabled = false;
            malfunctioningTollBoothCh.IsChecked = false;

        }

        void InitializeControllers()
        {
            sectionCotroller = new(serviceBuilder.SectionService);
            tollStationController = new(serviceBuilder.TollStationService);
            priceListController = new(serviceBuilder.PriceListService);
            locationController = new(serviceBuilder.LocationService);
            tollBoothController = new(serviceBuilder.TollBoothService);
            bossController = new(serviceBuilder.BossService);
            deviceController = new(serviceBuilder.DeviceService);

        }

        #region PriceList
        void InitializeCb()
        {
            foreach (Section section in sectionCotroller.Sections)
            {
                sectionCb.Items.Add(section.Id);
            }

            sectionCb.SelectedIndex = 0;
        }

        void DisplaySectionData(int sectionId)
        {
            Section section = sectionCotroller.FindById(sectionId);
            TollStation entranceStation = tollStationController.FindById(section.EntranceStation);
            TollStation exitStation = tollStationController.FindById(section.ExitStation);
            entryTb.Text = entranceStation.Name;
            exitTb.Text = exitStation.Name;
        }

        void DisplayPrices(int sectionId)
        {
            priceInformationLb.Items.Clear();
            List<Price> prices = priceListController.GetPricesBySection(sectionId);
            foreach (Price price in prices)
            {
                priceInformationLb.Items.Add("Vechile type: " + price.VehicleType1 + ", EUR: "
                    + price.PriceEur + ", RSD: " + price.PriceDin);
            }
        }

        private void sectionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sectionCb.SelectedIndex != -1)
            {
                DisplaySectionData((int)sectionCb.SelectedItem);
                DisplayPrices((int)sectionCb.SelectedItem);
                DisplayPriceListDate();
            }

        }

        private void DisplayPriceListDate()
        {
            PriceList activePriceList = priceListController.GetActive(DateTime.Today);

            startDateLbl.Content = activePriceList.StartDate.ToString("dd.MM.yyyy.");
        }
        #endregion

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

        #region TollStations

        void InitializeTollStationsCb()
        {
            tollStationCb.Items.Clear();
            foreach (TollStation tollStation in tollStationController.TollStations)
            {
                tollStationCb.Items.Add(tollStation.Id);
            }
            tollStationCb.SelectedIndex = 0;
        }

        void DisplayTollStationData()
        {
            TollStation tollStation = tollStationController.FindById((int)tollStationCb.SelectedItem);
            tollStationNameTb.Text = tollStation.Name;
            Boss boss = bossController.FindByJmbg(tollStation.BossJmbg);
            tollStationBossTb.Text = boss.Name + " " + boss.LastName;
            Location location = locationController.FindByZip(tollStation.LocationZip);
            countryTb.Text = location.Country;
            cityTb.Text = location.Name;
        }

        private void tollStationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tollStationCb.SelectedIndex != -1)
            {
                DisplayTollStationData();
            }
        }

        private void refreshTollStationsBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeTollStationsCb();
        }

        private void deleteTollStationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Toll station?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                tollStationController.Delete(tollStationController.FindById((int)tollStationCb.SelectedItem));
                MessageBox.Show("Toll station deleted sucessfully!");
                InitializeTollStationsCb();
            }
            else
            {
                return;
            }
        }

        private void updateTollStationBtn_Click(object sender, RoutedEventArgs e)
        {
            tollStationController.Update(tollStationNameTb.Text, tollStationController.FindById((int)tollStationCb.SelectedItem));
            MessageBox.Show("Toll station updated sucessfully!");
            DisplayTollStationData();
        }

        private void createTollStationBtn_Click(object sender, RoutedEventArgs e)
        {
            Window createNewStationWin = new CreateStationWindow(serviceBuilder);
            createNewStationWin.Show();
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
