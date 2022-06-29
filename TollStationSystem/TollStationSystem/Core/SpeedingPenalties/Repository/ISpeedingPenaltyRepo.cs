using System.Collections.Generic;
using TollStationSystem.Core.SpeedingPenalties.Model;

namespace TollStationSystem.Core.SpeedingPenalties.Repository
{
    public interface ISpeedingPenaltyRepo
    {
        List<SpeedingPenalty> SpeedingPenalties { get; }

        void Add(SpeedingPenalty speedingPenalty);

        SpeedingPenalty FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();
    }
}