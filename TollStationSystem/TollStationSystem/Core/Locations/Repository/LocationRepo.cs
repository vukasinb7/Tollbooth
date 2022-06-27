﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TollStationSystem.Core.Locations.Model;

namespace TollStationSystem.Core.Locations.Repository
{
    public class LocationRepo : ILocationRepo
    {
        List<Location> locations;
        string path;

        public LocationRepo()
        {
            path = "../../../Data/Locations.json";
            Load();
        }

        public List<Location> Locations { get => locations; }

        public void Load()
        {
            locations = JsonSerializer.Deserialize<List<Location>>(File.ReadAllText(path));
        }

        public void Serialize()
        {
            string locationsJson = JsonSerializer.Serialize(locations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, locationsJson);
        }

        public Location FindByZip(string zip)
        {
            foreach (Location location in locations)
                if (location.Zip == zip)
                    return location;

            return null;
        }

        public void Add(Location location)
        {
            locations.Add(location);
            Serialize();
        }
    }
}
