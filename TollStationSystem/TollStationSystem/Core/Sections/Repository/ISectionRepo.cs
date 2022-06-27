using System.Collections.Generic;
using TollStationSystem.Core.Sections.Model;

namespace TollStationSystem.Core.Sections.Repository
{
    interface ISectionRepo
    {
        List<Section> Sections { get; }

        void Add(Section section);

        void Delete(Section section);

        Section FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();
    }
}