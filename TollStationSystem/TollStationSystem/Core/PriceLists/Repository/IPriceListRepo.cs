using System.Collections.Generic;
using TollStationSystem.Core.PriceLists.Model;

namespace TollStationSystem.Core.PriceLists.Repository
{
    public interface IPriceListRepo
    {
        List<PriceList> PriceLists { get; }

        void Add(PriceList priceList);

        PriceList FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();
    }
}