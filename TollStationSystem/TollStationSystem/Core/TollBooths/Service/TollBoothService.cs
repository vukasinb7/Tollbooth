using System.Collections.Generic;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Repository;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.TollStations.Service;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.Core.TollBooths.Service
{
    public class TollBoothService : ITollBoothService
    {
        ITollBoothRepo tollBoothRepo;
        ITollStationService tollStationService;

        public TollBoothService(ITollBoothRepo tollBoothRepo, ITollStationService tollStationService)
        {
            this.tollBoothRepo = tollBoothRepo;
            this.tollStationService = tollStationService;
        }

        public List<TollBooth> TollBooths { get => tollBoothRepo.TollBooths; }

        public void Add(TollBoothDto tollBoothDto)
        {
            TollBooth tollBooth = new TollBooth(tollBoothDto);
            tollBoothRepo.Add(tollBooth);
        }

        public void Update(TollBoothDto tollBoothDto)
        {
            TollBooth tollBooth = FindById(tollBoothDto.TollStationId, tollBoothDto.Number);
            tollBooth.TollBoothType = tollBoothDto.TollBoothType;
            tollBooth.Malfunctioning = tollBoothDto.Malfunctioning;
            tollBooth.Devices = tollBoothDto.Devices;
            Serialize();
        }

        public TollBooth FindById(int stationId, int boothNumber)
        {
            return tollBoothRepo.FindById(stationId, boothNumber);
        }

        public int GenerateNum(TollStation tollStation)
        {
            return tollBoothRepo.GenerateNum(tollStation);
        }

        public void Load()
        {
            tollBoothRepo.Load();
        }

        public void Serialize()
        {
            tollBoothRepo.Serialize();
        }

        public bool AlreadyExist(int stationId, int number)
        {
            return tollBoothRepo.AlreadyExist(stationId, number);
        }
        public void Delete(int stationId, int number)
        {
            TollBooth tollBooth = FindById(stationId, number);
            tollBoothRepo.Delete(tollBooth);
            tollStationService.RemoveTollBooth(tollBooth, tollStationService.FindById(stationId));
        }
    }
}
