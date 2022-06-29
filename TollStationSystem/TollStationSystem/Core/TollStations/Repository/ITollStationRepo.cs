﻿using System.Collections.Generic;
using TollStationSystem.Core.TollStations.Model;

namespace TollStationSystem.Core.TollStations.Repository
{
    public interface ITollStationRepo
    {
        List<TollStation> TollStations { get; }

        void Add(TollStation tollStation);

        TollStation FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();
    }
}