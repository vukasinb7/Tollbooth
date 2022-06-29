﻿using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;

namespace TollStationSystem.Core.Users.Repository
{
    public interface IBossRepo
    {
        List<Boss> Bosses { get; }

        void Add(Boss boss);

        Boss FindByJmbg(string jmbg);

        void Load();

        void Serialize();
    }
}