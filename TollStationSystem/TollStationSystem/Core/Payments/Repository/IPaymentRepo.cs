using System.Collections.Generic;
using TollStationSystem.Core.Payments.Model;

namespace TollStationSystem.Core.Payments.Repository
{
    public interface IPaymentRepo
    {
        List<Payment> Payments { get; }

        void Add(Payment payment);

        Payment FindById(int id);

        int GenerateId();

        void Load();

        void Serialize();
    }
}