using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;

namespace SmartWalletWebApi.Repositories;

public class IncomeRepository : IincomeRepository
{
    private AppDbContext context;

    public IncomeRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<int> AddIncome(Income income)
    {
        await context.Incomes.AddAsync(income);
        await context.SaveChangesAsync();
        return income.Id;
    }

    public async Task<IEnumerable<Income>> AllIncomesAsync()
    {
        var incomes = await context.Incomes.ToListAsync();
        return incomes;
    }

    public async Task<IEnumerable<Income>> GetByUserId(int id)
    {
        return context.Incomes.Where(f => f.UserId == id).ToList();
    }

    public async Task<Income?> GetByid(int id)
    {
        return await context.Incomes.FirstOrDefaultAsync(f => f.Id == id);
    }
}
