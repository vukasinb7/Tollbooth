﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TollStationSystem.Core.SpeedingPenalties.Model;

namespace TollStationSystem.Core.SpeedingPenalties.Repository
{
    public class SpeedingPenaltyRepo : ISpeedingPenaltyRepo
    {
        List<SpeedingPenalty> speedingPenalties;
        string path;

        public List<SpeedingPenalty> SpeedingPenalties { get => speedingPenalties; }

        public SpeedingPenaltyRepo()
        {
            path = "../../../Data/SpeedingPenalties.json";
            Load();
        }

        public void Load()
        {
            speedingPenalties = JsonSerializer.Deserialize<List<SpeedingPenalty>>(File.ReadAllText(path));
        }

        public void Serialize()
        {
            string speedingPenaltiesJson = JsonSerializer.Serialize(speedingPenalties,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, speedingPenaltiesJson);
        }

        public int GenerateId()
        {
            return speedingPenalties[^1].Id + 1;
        }

        public SpeedingPenalty FindById(int id)
        {
            foreach (SpeedingPenalty speedingPenalty in speedingPenalties)
                if (speedingPenalty.Id == id)
                    return speedingPenalty;

            return null;
        }

        public void Add(SpeedingPenalty speedingPenalty)
        {
            speedingPenalties.Add(speedingPenalty);
            Serialize();
        }
    }
}
