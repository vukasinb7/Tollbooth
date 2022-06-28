using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Locations.Model;
using TollStationSystem.Core.Locations.Service;

namespace TollStationSystem.GUI.Controllers.Locations
{
    public class LocationController
    {
        ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public List<Location> Locations { get => locationService.Locations; }

        public void Add(Location location)
        {
            locationService.Add(location);
        }

        public Location FindByZip(string zip)
        {
            return locationService.FindByZip(zip);
        }

        public void Load()
        {
            locationService.Load();
        }

        public void Serialize()
        {
            locationService.Serialize();
        }
    }
}
