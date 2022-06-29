﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Payments.Model;
using TollStationSystem.Core.Payments.Service;

namespace TollStationSystem.GUI.Controllers.Payments
{
    public class PaymentController
    {
        IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public List<Payment> Payments { get => paymentService.Payments; }

        public void Add(Payment payment)
        {
            paymentService.Add(payment);
        }

        public Payment FindById(int id)
        {
            return paymentService.FindById(id);
        }

        public int GenerateId()
        {
            return paymentService.GenerateId();
        }

        public void Load()
        {
            paymentService.Load();
        }

        public void Serialize()
        {
            paymentService.Serialize();
        }
    }
}