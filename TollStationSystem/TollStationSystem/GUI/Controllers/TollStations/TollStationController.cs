﻿using System.Collections.Generic;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Service;

namespace TollStationSystem.GUI.Controllers.TollStations
{
    public class TollStationController
    {
        ITollStationService tollStationService;

        public TollStationController(ITollStationService tollStationService)
        {
            this.tollStationService = tollStationService;
        }

        public List<TollStation> TollStations { get => tollStationService.TollStations; }

        public void Add(TollStation tollStation)
        {
            tollStationService.Add(tollStation);
        }

        public TollStation FindById(int id)
        {
            return tollStationService.FindById(id);
        }

        public int GenerateId()
        {
            return tollStationService.GenerateId();
        }

        public void Load()
        {
            tollStationService.Load();
        }

        public void Serialize()
        {
            tollStationService.Serialize();
        }

        public TollStation FindByBoss(string jmbg)
        {
            return tollStationService.FindByBoss(jmbg);
        }
    }
}
