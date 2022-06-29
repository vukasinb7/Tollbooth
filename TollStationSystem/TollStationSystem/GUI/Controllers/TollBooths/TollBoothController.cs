using System.Collections.Generic;
using TollStationSystem.Core.TollBooths.Model;
using TollStationSystem.Core.TollBooths.Service;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.GUI.DTO;

namespace TollStationSystem.GUI.Controllers.TollBooths
{
    public class TollBoothController
    {
        ITollBoothService tollBoothService;

        public TollBoothController(ITollBoothService tollBoothService)
        {
            this.tollBoothService = tollBoothService;
        }

        public List<TollBooth> TollBooths { get => tollBoothService.TollBooths; }

        public void Add(TollBoothDto tollBoothDto)
        {
            tollBoothService.Add(tollBoothDto);
        }

        public void Delete(int stationId, int number)
        {
            tollBoothService.Delete(stationId, number);
        }

        public TollBooth FindById(int stationId, int boothNumber)
        {
            return tollBoothService.FindById(stationId, boothNumber);
        }

        public int GenerateNum(TollStation tollStation)
        {
            return tollBoothService.GenerateNum(tollStation);
        }

        public void Load()
        {
            tollBoothService.Load();
        }

        public void Serialize()
        {
            tollBoothService.Serialize();
        }


        public bool AlreadyExist(int stationId, int number)
        {
            return tollBoothService.AlreadyExist(stationId, number);
        }

        public void Update(TollBoothDto tollBoothDto)
        {
            tollBoothService.Update(tollBoothDto);
        }

    }
}
