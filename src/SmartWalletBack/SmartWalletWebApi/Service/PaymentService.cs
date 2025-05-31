using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Dtos.Payment;
using SmartWalletWebApi.Enums;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Service;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        this.paymentRepository =
            paymentRepository
            ?? throw new ArgumentNullException(
                nameof(paymentRepository),
                "PaymentRepository cannot be null in PaymentService constructor."
            );
    }

    public async Task AddPayment(AddPaymentRequestDto payment)
    {
        if (payment == null)
        {
            throw new ArgumentNullException(
                nameof(payment),
                "Payment object is null in AddPayment method."
            );
        }

        if (payment.Amount <= 0)
        {
            throw new ArgumentException(
                "Payment amount must be greater than zero in AddPayment method."
            );
        }

        try
        {
            await paymentRepository.AddPayment(
                new Payment
                {
                    Amount = payment.Amount,
                    Type = Enum.TryParse<PaymentType>(payment.Type, true, out var type)
                        ? type
                        : PaymentType.Other,
                    UserId = payment.UserId,
                    Currency = payment.Currency,
                    Products = payment.Products,
                    SallerName = payment.SallerName,
                }
            );
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while saving the payment: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<GetPaymentResponseDto>> AllPaymentsAsync()
    {
        try
        {
            var payments = await paymentRepository.AllPaymentsAsync();
            if (payments == null)
            {
                throw new Exception(
                    "PaymentRepository returned null in AllPaymentsAsync. Expected a collection of payments."
                );
            }

            var result = payments.Select(payment => new GetPaymentResponseDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Type = payment.Type.ToString(),
                UserId = payment.UserId,

                Currency = payment.Currency,
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching all payments: {ex.Message}", ex);
        }
    }

    public async Task<GetPaymentResponseDto?> GetPaymentById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException(
                "Invalid payment ID in GetPaymentById. ID must be greater than zero."
            );
        }

        try
        {
            var payment = await paymentRepository.GetPaymentById(id);
            if (payment == null)
            {
                throw new Exception($"No payment found with ID {id} in GetPaymentById.");
            }

            return new GetPaymentResponseDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Type = payment.Type.ToString(),
                UserId = payment.UserId,

                Currency = payment.Currency,
            };
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"An error occurred while fetching payment by ID {id}: {ex.Message}",
                ex
            );
        }
    }

    public async Task<IEnumerable<GetPaymentResponseDto>> GetPaymentByUserId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException(
                "Invalid payment ID in GetPaymentByUserId. ID must be greater than zero."
            );
        }

        try
        {
            var payment = await paymentRepository.GetPaymentByUserId(id);
            if (payment == null)
            {
                throw new Exception($"No payment found with ID {id} in GetPaymentByUserId.");
            }

            var result = payment.Select(payment => new GetPaymentResponseDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Type = payment.Type.ToString(),
                UserId = payment.UserId,

                Currency = payment.Currency,
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"An error occurred while fetching payment by ID {id}: {ex.Message}",
                ex
            );
        }
    }
}
