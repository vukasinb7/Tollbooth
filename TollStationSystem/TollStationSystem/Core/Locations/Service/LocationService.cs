using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Locations.Model;
using TollStationSystem.Core.Locations.Repository;

namespace TollStationSystem.Core.Locations.Service
{
    public class LocationService : ILocationService
    {
        ILocationRepo locationRepo;

        public LocationService(ILocationRepo locationRepo)
        {
            this.locationRepo = locationRepo;
        }

        public List<Location> Locations { get => locationRepo.Locations; }

        public Location FindByZip(string zip)
        {
            return locationRepo.FindByZip(zip);
        }

        public void Load()
        {
            locationRepo.Load();
        }


        public void Serialize()
        {
            locationRepo.Serialize();
        }

        public void Add(Location location)
        {
            locationRepo.Add(location);
        }
    }
}
