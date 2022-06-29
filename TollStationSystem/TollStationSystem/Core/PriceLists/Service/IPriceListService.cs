using System.Collections.Generic;
using TollStationSystem.Core.PriceLists.Model;

namespace TollStationSystem.Core.PriceLists.Service
{
    public interface IPriceListService
    {
        List<PriceList> PriceLists { get; }

        void Add(PriceList priceList);
        PriceList FindById(int id);
        int GenerateId();
        void Load();
        void Serialize();
        Price GetPriceBySectionId(int sectionId, VehicleType vt);
    }
}