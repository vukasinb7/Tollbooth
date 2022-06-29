using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.Devices.Service;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Repository;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Service;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.Core.TollBooths.Service
{
    public class TollBoothService : ITollBoothService
    {
        ITollBoothRepo tollBoothRepo;
        ITollStationService tollStationService;
        IDeviceService deviceService;

        public TollBoothService(ITollBoothRepo tollBoothRepo, ITollStationService tollStationService, IDeviceService deviceService)
        {
            this.tollBoothRepo = tollBoothRepo;
            this.tollStationService = tollStationService;
            this.deviceService = deviceService;
        }

        public List<TollBooth> TollBooths { get => tollBoothRepo.TollBooths; }

        public void Add(TollBoothDto tollBoothDto)
        {
            TollBooth tollBooth = new TollBooth(tollBoothDto);
            tollBoothRepo.Add(tollBooth);
        }

        public void Update(TollBoothDto tollBoothDto)
        {
            TollBooth tollBooth = FindById(tollBoothDto.TollStationId, tollBoothDto.Number);
            tollBooth.TollBoothType = tollBoothDto.TollBoothType;
            tollBooth.Malfunctioning = tollBoothDto.Malfunctioning;
            tollBooth.Devices = tollBoothDto.Devices;
            Serialize();
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


        public bool AlreadyExist(int stationId, int number)
        {
            return tollBoothRepo.AlreadyExist(stationId, number);
        }
        
        public void Delete(int stationId, int number)
        {
            TollBooth tollBooth = FindById(stationId, number);
            tollBoothRepo.Delete(tollBooth);
            tollStationService.RemoveTollBooth(tollBooth, tollStationService.FindById(stationId));
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

        public List<Device> FindDevices(int stationId, int boothNumber)
        {
            List<Device> filtered = new();

            TollBooth tollBooth = FindById(stationId, boothNumber);
            foreach (int deviceId in tollBooth.Devices)
                filtered.Add(deviceService.FindById(deviceId));

            return filtered;
        }

        public List<Device> FindNonRampDevices(int stationId, int boothNumber)
        {
            List<Device> filtered = new();

            TollBooth tollBooth = FindById(stationId, boothNumber);
            foreach (int deviceId in tollBooth.Devices)
            {
                Device device = deviceService.FindById(deviceId);
                if (device.DeviceType != DeviceType.RAMP)
                    filtered.Add(device);
            }

            return filtered;
        }
    }
}
