using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.PriceLists.Model;
using TollStationSystem.Core.PriceLists.Repository;

namespace TollStationSystem.Core.PriceLists.Service
{
    public class PriceListService : IPriceListService
    {
        IPriceListRepo priceListRepo;

        public PriceListService(IPriceListRepo priceListRepo)
        {
            this.priceListRepo = priceListRepo;
        }

        public List<PriceList> PriceLists { get => priceListRepo.PriceLists; }

        public PriceList FindById(int id)
        {
            return priceListRepo.FindById(id);
        }

        public int GenerateId()
        {
            return priceListRepo.GenerateId();
        }

        public void Load()
        {
            priceListRepo.Load();
        }

        public void Serialize()
        {
            priceListRepo.Serialize();
        }

        public void Add(PriceList priceList)
        {
            priceListRepo.Add(priceList);
        }
    }
}
