using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Service;

namespace TollStationSystem.GUI.Controllers.TollStations
{
    public class TollStationController
    {
        ITollStationService tollStationService;

        public TollStationController(ITollStationService tollStationService)
        {
            this.tollStationService = tollStationService;
        }

        public List<TollStation> TollStations { get => tollStationService.TollStations; }

        public void Add(TollStation tollStation)
        {
            tollStationService.Add(tollStation);
        }

        public TollStation FindById(int id)
        {
            return tollStationService.FindById(id);
        }

        public int GenerateId()
        {
            return tollStationService.GenerateId();
        }

        public void Load()
        {
            tollStationService.Load();
        }

        public void Serialize()
        {
            tollStationService.Serialize();
        }

        public TollStation FindByWorkerJmbg(string jmg)
        {
            return tollStationService.FindByWorkerJmbg(jmg);
        }

        public Dictionary<Device, int> FindRamps(int stationId)
        {
            return tollStationService.FindRamps(stationId);
        }

        public Dictionary<Device, int> FindDevices(int stationId)
        {
            return tollStationService.FindDevices(stationId);
        }

        public Dictionary<Device, int> FindNonRampDevices(int stationId)
        {
            return tollStationService.FindNonRampDevices(stationId);
        }
    }
}
