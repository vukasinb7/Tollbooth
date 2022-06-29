using System.Collections.Generic;
using TollStationSystem.Core.Locations.Model;

namespace TollStationSystem.Core.Locations.Service
{
    public interface ILocationService
    {
        List<Location> Locations { get; }

        void Add(Location location);
        Location FindByZip(string zip);
        void Load();
        void Serialize();
    }
}