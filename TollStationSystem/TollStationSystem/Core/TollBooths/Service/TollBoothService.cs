﻿using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.Devices.Service;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Repository;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollBooths.Service
{
    public class TollBoothService : ITollBoothService
    {
        ITollBoothRepo tollBoothRepo;
        IDeviceService deviceService;

        public TollBoothService(ITollBoothRepo tollBoothRepo, IDeviceService deviceService)
        {
            this.tollBoothRepo = tollBoothRepo;
            this.deviceService = deviceService;
        }

        public List<TollBooth> TollBooths { get => tollBoothRepo.TollBooths; }

        public void Add(TollBooth tollBooth)
        {
            tollBoothRepo.Add(tollBooth);
        }

        public TollBooth FindById(int stationId, int boothNumber)
        {
            return tollBoothRepo.FindById(stationId, boothNumber);
        }

        public int GenerateNum(TollStation tollStation)
        {
            return tollBoothRepo.GenerateNum(tollStation);
        }

        public void Load()
        {
            tollBoothRepo.Load();
        }

        public void Serialize()
        {
            tollBoothRepo.Serialize();
        }

        public Device FindBoothRamp(int stationId, int boothNumber)
        {
            TollBooth tollBooth = FindById(stationId, boothNumber);
            foreach (int deviceId in tollBooth.Devices)
            {
                Device device = deviceService.FindById(deviceId);
                if (device.DeviceType == DeviceType.RAMP)
                    return device;
            }

            return null;
        }

        public List<Device> DevicesByBooth(int stationId, int boothNumber)
        {
            List<Device> filtered = new();

            TollBooth tollBooth = FindById(stationId, boothNumber);
            foreach (int deviceId in tollBooth.Devices)
                filtered.Add(deviceService.FindById(deviceId));

            return filtered;
        }
    }
}
