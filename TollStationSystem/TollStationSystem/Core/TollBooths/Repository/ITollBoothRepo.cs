using System.Collections.Generic;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollBooths.Repository
{
    public interface ITollBoothRepo
    {
        List<TollBooth> TollBooths { get; }

        void Add(TollBooth tollBooth);

        TollBooth FindById(int stationId, int boothNumber);

        int GenerateNum(TollStation tollStation);

        void Load();

        void Serialize();
    }
}