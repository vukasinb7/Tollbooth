using System.Collections.Generic;
using TollStationSystem.Core.Payments.Model;
using TollStationSystem.Core.Payments.Repository;

namespace TollStationSystem.Core.Payments.Service
{
    class PaymentService : IPaymentService
    {
        IPaymentRepo paymentRepo;

        public PaymentService(IPaymentRepo paymentRepo)
        {
            this.paymentRepo = paymentRepo;
        }

        public List<Payment> Payments { get => paymentRepo.Payments; }

        public IPaymentRepo PaymentRepo => paymentRepo;

        public int GenerateId()
        {
            return paymentRepo.GenerateId();
        }

        public Payment FindById(int id)
        {
            return paymentRepo.FindById(id);
        }

        public void Load()
        {
            paymentRepo.Load();
        }

        public void Serialize()
        {
            paymentRepo.Serialize();
        }

        public void Add(Payment payment)
        {
            paymentRepo.Add(payment);
        }
    }
}
