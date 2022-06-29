using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollBooths.Service
{
    public interface ITollBoothService
    {
        List<TollBooth> TollBooths { get; }

        void Add(TollBooth tollBooth);

        TollBooth FindById(int stationId, int boothNumber);

        int GenerateNum(TollStation tollStation);

        void Load();

        void Serialize();

        Device FindBoothRamp(int stationId, int boothNumber);

        List<Device> FindDevices(int stationId, int boothNumber);

        List<Device> FindNonRampDevices(int stationId, int boothNumber);
    }
}