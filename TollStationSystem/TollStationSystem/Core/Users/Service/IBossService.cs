using System.Collections.Generic;
using TollStationSystem.Core.DeploymentHistory.Model;
using TollStationSystem.Core.Users.Model;

namespace TollStationSystem.Core.Users.Service
{
    public interface IBossService
    {
        List<Boss> Bosses { get; }

        void Add(Boss boss);

        Boss FindByJmbg(string jmbg);

        void Load();

        void Serialize();

        void RemoveFromStation(string bossJmbg);

        DeploymentHistoryRecord PutToStation(int stationId, Boss boss);
    }
}