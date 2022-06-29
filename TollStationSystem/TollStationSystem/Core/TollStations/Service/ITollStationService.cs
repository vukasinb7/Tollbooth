using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollStations.Service
{
    public interface ITollStationService
    {
        List<TollStation> TollStations { get; }

        void Add(TollStation tollStation);

        TollStation FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();

        TollStation FindByWorkerJmbg(string jmbg);

        Dictionary<Device, int> FindRamps(int stationId);

        Dictionary<Device, int> FindDevices(int stationId);

        Dictionary<Device, int> FindNonRampDevices(int stationId);
    }
}