using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Sections.Model;
using TollStationSystem.Core.Sections.Service;

namespace TollStationSystem.GUI.Controllers.Sections
{
    public class SectionController
    {
        ISectionService sectionService;

        public SectionController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }

        public List<Section> Sections { get => sectionService.Sections; }

        public void Add(Section section)
        {
            sectionService.Add(section);
        }

        public Section FindById(int id)
        {
            return sectionService.FindById(id);
        }

        public int GenerateId()
        {
            return sectionService.GenerateId();
        }

        public void Load()
        {
            sectionService.Load();
        }

        public void Serialize()
        {
            sectionService.Serialize();
        }

        public void Delete(Section section)
        {
            sectionService.Delete(section);
        }
    }
}
