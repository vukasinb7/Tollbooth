﻿using System.Collections.Generic;
using System.Linq;
using TollStationSystem.Core.Sections.Model;
using TollStationSystem.Core.Sections.Service;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Service;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Repository;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Core.Users.Service;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.Core.TollStations.Service
{
    public class TollStationService : ITollStationService
    {
        ITollStationRepo tollStationRepo;

        private IBossService bossService;
        private ITollBoothService tollBoothService;
        private ISectionService sectionService;

        public TollStationService(ITollStationRepo tollStationRepo, IBossService bossService,
            ITollBoothService tollBoothService, ISectionService sectionService)
        {
            this.tollStationRepo = tollStationRepo;
            this.bossService = bossService;
            this.tollBoothService = tollBoothService;
            this.sectionService = sectionService;
        }

        public List<TollStation> TollStations { get => tollStationRepo.TollStations; }

        public void Add(TollStation tollStation)
        {
            tollStationRepo.Add(tollStation);
        }

        public void Add(TollStationDto tollStationDto)
        {
            TollStation tollStation = new TollStation(tollStationDto);
            Add(tollStation);
        }

        public TollStation FindById(int id)
        {
            return tollStationRepo.FindById(id);
        }

        public int GenerateId()
        {
            return tollStationRepo.GenerateId();
        }

        public void Load()
        {
            tollStationRepo.Load();
        }

        public void Serialize()
        {
            tollStationRepo.Serialize();
        }

        public void RemoveTollBooth(TollBooth tollBooth, TollStation tollStation)
        {
            tollStation.TollBooths.Remove(tollBooth.Number);
            Serialize();
        }

        public void Delete(TollStation tollStation)
        {
            List<int> tollBoothsToDelete = new();
            foreach (int number in tollStation.TollBooths)
            {
                tollBoothsToDelete.Add(number);
            }
            foreach (int number in tollBoothsToDelete)
            {
                tollBoothService.Delete(tollStation.Id, number);
            }
            bossService.RemoveFromStation(tollStation.BossJmbg);
            List<int> sectionsToDelete = new();
            foreach (Section section in sectionService.Sections)
            {
                if (section.EntranceStation == tollStation.Id || section.ExitStation == tollStation.Id)
                {
                    sectionsToDelete.Add(section.Id);
                }
            }
            foreach (int sectionId in sectionsToDelete)
            {
                sectionService.Delete(sectionService.FindById(sectionId));
            }
            TollStations.Remove(tollStation);
            Serialize();
        }

        public void Update(string name, TollStation tollStation)
        {
            tollStation.Name = name;
            Serialize();
        }

        public List<Boss> AvailableBosses()
        {
            List<Boss> availableBosses = new();
            foreach (Boss boss in bossService.Bosses)
            {
                if (!TollStations.Any(x => x.BossJmbg == boss.Jmbg))
                {
                    availableBosses.Add(boss);
                }
            }

            return availableBosses;
        }
    }
}
