using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.Core.TollBooths.Service
{
    public interface ITollBoothService
    {
        List<TollBooth> TollBooths { get; }

        void Add(GUI.DTO.TollBoothDto tollBooth);

        Model.TollBooth FindById(int stationId, int boothNumber);

        int GenerateNum(TollStation tollStation);

        void Load();

        void Serialize();

        List<TollBooth> GetAllFromStation(TollStation tollStation);

        void Fix(TollBooth tollBooth);
        
        public bool AlreadyExist(int stationId, int number);
        
        public void Delete(int stationId, int number);

        void Update(TollBoothDto tollBoothDto);

        Device FindBoothRamp(int stationId, int boothNumber);

        List<Device> FindDevices(int stationId, int boothNumber);

        List<Device> FindNonRampDevices(int stationId, int boothNumber);

    }
}