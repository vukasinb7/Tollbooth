using System.Collections.Generic;
using TollStationSystem.Core.DeploymentHistory.Model;

namespace TollStationSystem.Core.DeploymentHistory.Service
{
    public interface IDeploymentHistoryService
    {
        List<DeploymentHistoryRecord> DeploymentHistoryRecords { get; }

        void Add(DeploymentHistoryRecord deploymentHistoryRecord);
        List<DeploymentHistoryRecord> FilterByBossJmbg(string jmbg);
        List<DeploymentHistoryRecord> FilterByStationId(int id);
        void Load();
        void Serialize();
    }
}