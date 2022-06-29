using System.Collections.Generic;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Repository;

namespace TollStationSystem.Core.TollStations.Service
{
    public class TollStationService : ITollStationService
    {
        ITollStationRepo tollStationRepo;

        public TollStationService(ITollStationRepo tollStationRepo)
        {
            this.tollStationRepo = tollStationRepo;
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
    }
}
