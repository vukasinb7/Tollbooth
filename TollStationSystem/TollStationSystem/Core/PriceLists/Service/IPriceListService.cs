using System;
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
        PriceList GetActive(DateTime date);

        Price GetPriceBySectionId(int sectionId, VehicleType vt);
        Price GetPriceBySectionId(int sectionId, VehicleType vt, DateTime date);
        void Load();
        void Serialize();
        List<PriceList> SortedByStartDate();
    }
}