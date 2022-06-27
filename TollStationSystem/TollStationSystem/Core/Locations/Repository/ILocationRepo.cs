using System.Collections.Generic;
using TollStationSystem.Core.Locations.Model;

namespace TollStationSystem.Core.Locations.Repository
{
    public interface ILocationRepo
    {
        List<Location> Locations { get; }

        void Add(Location location);
        Location FindByZip(string zip);
        void Load();
        void Serialize();
    }
}