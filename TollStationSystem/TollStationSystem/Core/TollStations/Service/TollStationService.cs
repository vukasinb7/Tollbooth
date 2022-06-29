using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Service;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Repository;

namespace TollStationSystem.Core.TollStations.Service
{
    public class TollStationService : ITollStationService
    {
        ITollStationRepo tollStationRepo;
        ITollBoothService tollBoothService;

        public TollStationService(ITollStationRepo tollStationRepo, ITollBoothService tollBoothService)
        {
            this.tollStationRepo = tollStationRepo;
            this.tollBoothService = tollBoothService;
        }

        public List<TollStation> TollStations { get => tollStationRepo.TollStations; }

        public void Add(TollStation tollStation)
        {
            tollStationRepo.Add(tollStation);
        }

        public TollStation FindById(int id)
        {
            return tollStationRepo.FindById(id);
        }

        public int GenerateId()
        {
            return tollStationRepo.GenerateId();
        }

        public void Load()
        {
            tollStationRepo.Load();
        }

        public void Serialize()
        {
            tollStationRepo.Serialize();
        }

        public TollStation FindByWorkerJmbg(string jmbg)
        {
            foreach (TollStation station in TollStations)
                foreach (string workerId in station.Users)
                    if (workerId == jmbg)
                        return station;
            return null;
        }

        public Dictionary<Device, int> FindRamps(int stationId)
        {
            Dictionary<Device, int> ramps = new();
            TollStation tollStation = FindById(stationId);
            
            foreach (int boothNumber in tollStation.TollBooths)
            {
                Device ramp = tollBoothService.FindBoothRamp(stationId, boothNumber);
                ramps.Add(ramp, boothNumber);
            }

            return ramps;
        }

        public Dictionary<Device, int> FindDevices(int stationId)
        {
            Dictionary<Device, int> devicesData = new();
            TollStation tollStation = FindById(stationId); 

            foreach (int boothNumber in tollStation.TollBooths)
            {
                List<Device> devices = tollBoothService.FindDevices(stationId, boothNumber);
                foreach (Device device in devices)
                    devicesData.Add(device, boothNumber);
            }

            return devicesData;
        }

        public Dictionary<Device, int> FindNonRampDevices(int stationId)
        {
            Dictionary<Device, int> devicesData = new();
            TollStation tollStation = FindById(stationId);

            foreach (int boothNumber in tollStation.TollBooths)
            {
                List<Device> devices = tollBoothService.FindDevices(stationId, boothNumber);
                foreach (Device device in devices)
                    if (device.DeviceType != DeviceType.RAMP)
                        devicesData.Add(device, boothNumber);
            }

            return devicesData;
        }
    }
}
