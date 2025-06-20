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
                Type = Enum.TryParse<IncomeType>(dto.Type, true, out var type)
                    ? type
                    : IncomeType.Other,
                UserId = dto.UserId,
                DateReceived = DateTime.UtcNow,
                Currency = dto.Currency,
            };

            int id = await incomeRepository.AddIncome(income);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while saving the income: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<GetIncomeResponseDto>> AllIncomesAsync()
    {
        return (await incomeRepository.AllIncomesAsync()).Select(income => new GetIncomeResponseDto
        {
            Id = income.Id,
            Amount = income.Amount,
            Type = income.Type.ToString(),
            UserId = income.UserId,
            DateReceived = income.DateReceived,
            Currency = income.Currency,
        });
    }

    public async Task<IEnumerable<GetIncomeResponseDto>> GetByUserId(int id)
    {
        return (await incomeRepository.GetByUserId(id)).Select(income => new GetIncomeResponseDto
        {
            Id = income.Id,
            Amount = income.Amount,
            Type = income.Type.ToString(),
            UserId = income.UserId,
            DateReceived = income.DateReceived,
            Currency = income.Currency,
        });
        ;
    }

    public async Task<GetIncomeResponseDto?> GetByid(int id)
    {
        var res = await incomeRepository.GetByid(id);
        return res == null
            ? null
            : new GetIncomeResponseDto
            {
                Id = res.Id,
                Amount = res.Amount,
                Type = res.Type.ToString(),
                UserId = res.UserId,
                DateReceived = res.DateReceived,
                Currency = res.Currency,
            };
    }

    public void BulkAdd(List<AddIncomeRequestDto> dtos)
    {
        incomeRepository.BulkCreate(
            dtos.Select(i => new Income
                {
                    Amount = i.Amount,
                    Type = Enum.TryParse<IncomeType>(i.Type, true, out var type)
                        ? type
                        : IncomeType.Other,
                    Currency = i.Currency,
                    UserId = i.UserId,
                })
                .ToList()
        );
    }
}
