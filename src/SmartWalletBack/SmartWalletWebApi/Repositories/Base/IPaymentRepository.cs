using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.Repositories.Base;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> AllPaymentsAsync();
    Task<Payment?> GetPaymentById(int id);
    Task AddPayment(Payment payment);
    Task<IEnumerable<Payment>> GetPaymentByUserId(int id);
    
    
}
