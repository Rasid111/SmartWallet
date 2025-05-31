using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Dtos.Income;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Repositories;

public class IncomeService : IIncomeService
{
    IIncomeRepository incomeRepository;

    public IncomeService(IIncomeRepository incomeRepository)
    {
        this.incomeRepository =
            incomeRepository
            ?? throw new ArgumentNullException(
                nameof(incomeRepository),
                "IncomeRepository cannot be null in IncomeService constructor."
            );
    }

    public async Task<int> AddIncome(AddIncomeRequestDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(
                nameof(dto),
                "Income object is null in AddIncome method."
            );
        }

        if (dto.Amount <= 0)
        {
            throw new ArgumentException(
                "Income amount must be greater than zero in AddIncome method."
            );
        }

        try
        {
            var income = new Income
            {
                Amount = dto.Amount,
                Type = Enum.TryParse<IncomeType>(dto.Type, true, out var type) ? type : IncomeType.Other,
                UserId = dto.UserId,
                DateReceived = DateTime.UtcNow,
                Currency = dto.Currency
            };

            int id = await incomeRepository.AddIncome(income);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while saving the income: {ex.Message}", ex);
        }
    }

    // public async Task<Income> AddIncome(IncomeDto dto)
    // {
    //     if (dto == null)
    //         throw new ArgumentNullException(nameof(dto), "Income object is null.");

    //     if (dto.Amount <= 0)
    //         throw new ArgumentException("Income amount must be greater than zero.");

    //     var income = new Income
    //     {
    //         Amount = dto.Amount,
    //         Type = dto.Type,
    //         UserId = dto.UserId,
    //         DateReceived = DateTime.UtcNow,
    //     };

    //     await incomeRepository.AddIncome(income);

    //     return income;
    // }

    public async Task<IEnumerable<Income>> AllIncomesAsync()
    {
        return await incomeRepository.AllIncomesAsync();
    }

    public async Task<IEnumerable<Income>> GetByUserId(int id)
    {
        return await incomeRepository.GetByUserId(id);
    }

    public async Task<Income?> GetByid(int id)
    {
        return await incomeRepository.GetByid(id);
    }
}
