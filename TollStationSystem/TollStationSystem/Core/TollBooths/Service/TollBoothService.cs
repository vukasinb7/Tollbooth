using System.Collections.Generic;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Repository;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollBooths.Service
{
    public class TollBoothService : ITollBoothService
    {
        ITollBoothRepo tollBoothRepo;

        public TollBoothService(ITollBoothRepo tollBoothRepo)
        {
            this.tollBoothRepo = tollBoothRepo;
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

        public List<TollBooth> GetAllFromStation(TollStation tollStation)
        {
            List<TollBooth> tollBooths = new();
            foreach (int tollBoothNumber in tollStation.TollBooths)
            {
                tollBooths.Add(FindById(tollStation.Id, tollBoothNumber));
            }
            return tollBooths;
        }

        public void Fix(TollBooth tollBooth)
        {
            tollBooth.Malfunctioning = false;
            Serialize();
        }
    }
}
