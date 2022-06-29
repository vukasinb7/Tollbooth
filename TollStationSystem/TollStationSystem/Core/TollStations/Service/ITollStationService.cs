using System.Collections.Generic;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.Core.TollStations.Service
{
    public interface ITollStationService
    {
        List<TollStation> TollStations { get; }

        void Add(TollStation tollStation);

        void Add(TollStationDto tollStationDto);

        TollStation FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();

        TollStation FindByBoss(string jmbg);
        
        void RemoveTollBooth(TollBooth tollBooth, TollStation tollStation);

        void Delete(TollStation tollStation);

        void Update(string name, TollStation tollStation);

        List<Boss> AvailableBosses();

        TollStation FindByWorkerJmbg(string jmbg);

        Dictionary<Device, int> FindRamps(int stationId);

        Dictionary<Device, int> FindDevices(int stationId);

        Dictionary<Device, int> FindNonRampDevices(int stationId);

    }
}