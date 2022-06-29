﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TollStationSystem.Core.DeploymentHistory.Model
{
    public class DeploymentHistoryRecord
    {
        string bossJmbg;
        int tollStationId;
        DateTime deploymentDate;

        public DeploymentHistoryRecord() { }

        public DeploymentHistoryRecord(string bossJmbg, int tollStationId, DateTime deploymentDate)
        {
            this.bossJmbg = bossJmbg;
            this.tollStationId = tollStationId;
            this.deploymentDate = deploymentDate;
        }

        [JsonPropertyName("bossJmbg")]
        public string BossJmbg { get => bossJmbg; set => bossJmbg = value; }

        [JsonPropertyName("tollStationId")]
        public int TollStationId { get => tollStationId; set => tollStationId = value; }

        [JsonPropertyName("deploymentDate")]
        public DateTime DeploymentDate { get => deploymentDate; set => deploymentDate = value; }

        public override string ToString()
        {
            return "DeploymentHistoryRecord[bossJmbg: " + bossJmbg + ", tollStationId: " + tollStationId
                   + ", deploymentDate: " + deploymentDate + "]";
        }
    }
}
