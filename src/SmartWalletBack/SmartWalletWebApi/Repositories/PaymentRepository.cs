using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;

namespace SmartWalletWebApi.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private AppDbContext context;

    public PaymentRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task AddPayment(Payment payment)
    {
        await context.Payments.AddAsync(payment);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Payment>> AllPaymentsAsync()
    {
        var payments = await context.Payments.Include(p => p.Products).ToListAsync();

        return payments;
    }

    public async Task<IEnumerable<Payment>> GetPaymentByUserId(int id)
    {
        return context.Payments.Where(f => f.UserId == id).ToList();
    }

    public async Task<Payment?> GetPaymentById(int id)
    {
        return await context.Payments.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task BulkCreate(List<Payment> addPaymentRequest)
    {
        context.Payments.AddRange(addPaymentRequest);
        context.SaveChanges();
    }
}
