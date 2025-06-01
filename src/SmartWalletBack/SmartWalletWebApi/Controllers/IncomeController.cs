using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SmartWalletWebApi.Dtos.Income;
using SmartWalletWebApi.Service.Base;

[Route("api/[controller]/[action]")]
[ApiController]
public class IncomeController : ControllerBase
{
    IIncomeService incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        this.incomeService = incomeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIncomesAsync()
    {
        try
        {
            var incomes = await incomeService.AllIncomesAsync();
            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching payments: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIncomeById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid income ID. It must be greater than zero.");
        }

        try
        {
            var income = await incomeService.GetByid(id);
            if (income == null)
            {
                return NotFound($"Income with ID {id} not found.");
            }

            return Ok(income);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching income by ID: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateIncome([FromBody] AddIncomeRequestDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Income data is missing in request body.");
        }

        try
        {
            int id = await incomeService.AddIncome(dto);
            return CreatedAtAction(nameof(GetIncomeById), new { id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating payment: {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetIncomeByUserId(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid Income ID. It must be greater than zero.");
        }

        try
        {
            var payment = await incomeService.GetByUserId(userId);
            if (payment == null)
            {
                return NotFound($"Income with ID {userId} not found.");
            }

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                $"An error occurred while fetching incoming by ID: {ex.Message}"
            );
        }
    }

    [HttpPost]
    public IActionResult BulkInsert(List<AddIncomeRequestDto> dtos)
    {
        incomeService.BulkAdd(dtos);
        return base.Ok();
    }
}

