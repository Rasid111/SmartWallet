using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Service;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository), "PaymentRepository cannot be null in PaymentService constructor.");
    }

    public async Task AddPayment(Payment payment)
    {
        if (payment == null)
        {
            throw new ArgumentNullException(nameof(payment), "Payment object is null in AddPayment method.");
        }

        if (payment.Amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero in AddPayment method.");
        }


        try
        {
            await paymentRepository.AddPayment(payment);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while saving the payment: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Payment>> AllPaymentsAsync()
    {
        try
        {
            var payments = await paymentRepository.AllPaymentsAsync();
            if (payments == null)
            {
                throw new Exception("PaymentRepository returned null in AllPaymentsAsync. Expected a collection of payments.");
            }

            return payments;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching all payments: {ex.Message}", ex);
        }
    }

    public async Task<Payment?> GetPaymentById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid payment ID in GetPaymentById. ID must be greater than zero.");
        }

        try
        {
            var payment = await paymentRepository.GetPaymentById(id);
            if (payment == null)
            {
                throw new Exception($"No payment found with ID {id} in GetPaymentById.");
            }

            return payment;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching payment by ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Payment?>> GetPaymentByUserId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid payment ID in GetPaymentByUserId. ID must be greater than zero.");
        }

        try
        {
            var payment = await paymentRepository.GetPaymentByUserId(id);
            if (payment == null)
            {
                throw new Exception($"No payment found with ID {id} in GetPaymentByUserId.");
            }

            return payment;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching payment by ID {id}: {ex.Message}", ex);
        }
    }
}