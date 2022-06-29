using System.Collections.Generic;
using TollStationSystem.Core.SpeedingPenalties.Model;
using TollStationSystem.Core.SpeedingPenalties.Service;

namespace TollStationSystem.GUI.Controllers.SpeedingPenalties
{
    public class SpeedingPenaltyController
    {
        ISpeedingPenaltyService speedingPenaltyService;

        public SpeedingPenaltyController(ISpeedingPenaltyService speedingPenaltyService)
        {
            this.speedingPenaltyService = speedingPenaltyService;
        }

        public List<SpeedingPenalty> SpeedingPenalties { get => speedingPenaltyService.SpeedingPenalties; }

        public void Add(SpeedingPenalty speedingPenalty)
        {
            speedingPenaltyService.Add(speedingPenalty);
        }

        public SpeedingPenalty FindById(int id)
        {
            return speedingPenaltyService.FindById(id);
        }

        public int GenerateId()
        {
            return speedingPenaltyService.GenerateId();
        }

        public void Load()
        {
            speedingPenaltyService.Load();
        }

        public void Serialize()
        {
            speedingPenaltyService.Serialize();
        }
    }
}
