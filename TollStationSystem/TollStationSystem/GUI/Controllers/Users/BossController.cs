using System.Collections.Generic;
using TollStationSystem.Core.DeploymentHistory.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Core.Users.Service;


namespace TollStationSystem.GUI.Controllers.Users
{
    public class BossController
    {
        IBossService bossService;

        public BossController(IBossService bossService)
        {
            this.bossService = bossService;
        }

        public List<Boss> Bosses { get => bossService.Bosses; }

        public void Add(Boss boss)
        {
            bossService.Add(boss);
        }

        public Boss FindByJmbg(string jmbg)
        {
            return bossService.FindByJmbg(jmbg);
        }

        public void Load()
        {
            bossService.Load();
        }

        public void Serialize()
        {
            bossService.Serialize();
        }

        public void RemoveFromStation(string bossJmbg)
        {
            bossService.RemoveFromStation(bossJmbg);
        }

        public DeploymentHistoryRecord PutToStation(int stationId, Boss boss)
        {
            return bossService.PutToStation(stationId, boss);
        }
    }

}
