using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Dtos.Payment;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.Service.Base;

public interface IPaymentService
{
    Task<IEnumerable<GetPaymentResponseDto>> AllPaymentsAsync();
    Task<GetPaymentResponseDto?> GetPaymentById(int id);
    Task AddPayment(AddPaymentRequestDto payment);
    Task<IEnumerable<GetPaymentResponseDto>> GetPaymentByUserId(int id);


}
