using System;
using System.Collections.Generic;
using TollStationSystem.Core.Payments.Model;
using TollStationSystem.Core.Payments.Repository;
using TollStationSystem.Core.PriceLists.Service;
using TollStationSystem.Core.TollStations.Service;

namespace TollStationSystem.Core.Payments.Service
{
    public interface IPaymentService
    {
        IPaymentRepo PaymentRepo { get; }
        List<Payment> Payments { get; }
        IPriceListService PriceListService { get; }
        ITollStationService TollStationService { get; }

        void Add(Payment payment);
        Payment FindById(int id);
        Tuple<float, float> FindSumOfPayments(DateTime start, DateTime end);
        Tuple<float, float> FindSumOfPayments(int tollStationId, DateTime start, DateTime end);
        int GenerateId();
        void Load();
        void Serialize();
    }
}