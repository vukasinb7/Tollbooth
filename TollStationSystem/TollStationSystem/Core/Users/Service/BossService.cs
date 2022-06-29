﻿using System;
using System.Collections.Generic;
using TollStationSystem.Core.DeploymentHistory.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Core.Users.Repository;


namespace TollStationSystem.Core.Users.Service
{
    public class BossService : IBossService
    {
        IBossRepo bossRepo;

        public BossService(IBossRepo bossRepo)
        {
            this.bossRepo = bossRepo;
        }

        public List<Boss> Bosses { get => bossRepo.Bosses; }

        public void Add(Boss boss)
        {
            bossRepo.Add(boss);
        }

        public Boss FindByJmbg(string jmbg)
        {
            return bossRepo.FindByJmbg(jmbg);
        }

        public void Load()
        {
            bossRepo.Load();
        }

        public void Serialize()
        {
            bossRepo.Serialize();
        }

        public void RemoveFromStation(string bossJmbg)
        {
            Boss boss = FindByJmbg(bossJmbg);
            boss.TollStationId = -1;
            Serialize();
        }

        public DeploymentHistoryRecord PutToStation(int stationId, Boss boss)
        {
            boss.TollStationId = stationId;
            Serialize();
            return new DeploymentHistoryRecord(boss.Jmbg, stationId, DateTime.Now);
        }

    }

}
