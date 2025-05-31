using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.Service.Base;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> AllPaymentsAsync();
    Task<Payment?> GetPaymentById(int id);
    Task AddPayment(Payment payment);
    Task<IEnumerable<Payment>> GetPaymentByUserId(int id);
}
