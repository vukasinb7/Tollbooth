using System.Collections.Generic;
using TollStationSystem.Core.Payments.Model;
using TollStationSystem.Core.Payments.Repository;

namespace TollStationSystem.Core.Payments.Service
{
    public interface IPaymentService
    {
        IPaymentRepo PaymentRepo { get; }
        List<Payment> Payments { get; }

        void Add(Payment payment);
        Payment FindById(int id);
        int GenerateId();
        void Load();
        void Serialize();
    }
}