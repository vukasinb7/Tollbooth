using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.DeploymentHistory.Model;
using TollStationSystem.Core.DeploymentHistory.Service;

namespace TollStationSystem.GUI.Controllers.DeploymentHistories
{
    public class DeploymentHistoryController
    {

        IDeploymentHistoryService deploymentHistoryService;

        public DeploymentHistoryController(IDeploymentHistoryService deploymentHistoryService)
        {
            this.deploymentHistoryService = deploymentHistoryService;
        }

        public List<DeploymentHistoryRecord> DeploymentHistoryRecords
        { get => deploymentHistoryService.DeploymentHistoryRecords; }

        public void Add(DeploymentHistoryRecord deploymentHistoryRecord)
        {
            deploymentHistoryService.Add(deploymentHistoryRecord);
        }

        public List<DeploymentHistoryRecord> FilterByBossJmbg(string jmbg)
        {
            return deploymentHistoryService.FilterByBossJmbg(jmbg);
        }

        public List<DeploymentHistoryRecord> FilterByStationId(int id)
        {
            return deploymentHistoryService.FilterByStationId(id);
        }

        public void Load()
        {
            deploymentHistoryService.Load();
        }

        public void Serialize()
        {
            deploymentHistoryService.Serialize();
        }
    }
}
