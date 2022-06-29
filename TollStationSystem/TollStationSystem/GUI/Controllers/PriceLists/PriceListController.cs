using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.PriceLists.Model;
using TollStationSystem.Core.PriceLists.Service;

namespace TollStationSystem.GUI.Controllers.PriceLists
{
    public class PriceListController
    {
        IPriceListService priceListService;

        public PriceListController(IPriceListService priceListService)
        {
            this.priceListService = priceListService;
        }

        public List<PriceList> PriceLists { get => priceListService.PriceLists; }

        public void Add(PriceList priceList)
        {
            priceListService.Add(priceList);
        }

        public PriceList FindById(int id)
        {
            return priceListService.FindById(id);
        }

        public int GenerateId()
        {
            return priceListService.GenerateId();
        }

        public void Load()
        {
            priceListService.Load();
        }

        public void Serialize()
        {
            priceListService.Serialize();
        }

        public PriceList GetActive(DateTime date)
        {
            return priceListService.GetActive(date);
        }

        public List<Price> GetPricesBySection(int sectionId)
        {
            return priceListService.GetPricesBySection(sectionId);
        }
    }
}
