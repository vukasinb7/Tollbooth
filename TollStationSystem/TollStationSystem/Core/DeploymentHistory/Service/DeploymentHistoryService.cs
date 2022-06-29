using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.DeploymentHistory.Model;
using TollStationSystem.Core.DeploymentHistory.Repository;

namespace TollStationSystem.Core.DeploymentHistory.Service
{
    public class DeploymentHistoryService : IDeploymentHistoryService
    {
        IDeploymentHistoryRepo deploymentHistoryRepo;

        public DeploymentHistoryService(IDeploymentHistoryRepo deploymentHistoryRepo)
        {
            this.deploymentHistoryRepo = deploymentHistoryRepo;
        }

        public List<DeploymentHistoryRecord> DeploymentHistoryRecords
        { get => deploymentHistoryRepo.DeploymentHistoryRecords; }

        public void Add(DeploymentHistoryRecord deploymentHistoryRecord)
        {
            deploymentHistoryRepo.Add(deploymentHistoryRecord);
        }

        public List<DeploymentHistoryRecord> FilterByBossJmbg(string jmbg)
        {
            return deploymentHistoryRepo.FilterByBossJmbg(jmbg);
        }

        public List<DeploymentHistoryRecord> FilterByStationId(int id)
        {
            return deploymentHistoryRepo.FilterByStationId(id);
        }

        public void Load()
        {
            deploymentHistoryRepo.Load();
        }

        public void Serialize()
        {
            deploymentHistoryRepo.Serialize();
        }
    }
}
