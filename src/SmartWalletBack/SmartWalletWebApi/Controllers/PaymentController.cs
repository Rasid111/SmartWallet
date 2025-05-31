using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Service;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService paymentService;
    private readonly OpenRouterService serviceAi;

    public PaymentController(IPaymentService paymentService, OpenRouterService serviceAi)
    {
        this.paymentService = paymentService;
        this.serviceAi = serviceAi;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        try
        {
            var payments = await paymentService.AllPaymentsAsync();
            return Ok(payments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching payments: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid payment ID. It must be greater than zero.");
        }

        try
        {
            var payment = await paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching payment by ID: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
    {
        if (payment == null)
        {
            return BadRequest("Payment data is missing in request body.");
        }

        try
        {
            await paymentService.AddPayment(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
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
    public async Task<IActionResult> GetPaymentByUserId(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid payment ID. It must be greater than zero.");
        }

        try
        {
            var payment = await paymentService.GetPaymentByUserId(userId);
            if (payment == null)
            {
                return NotFound($"Payment with ID {userId} not found.");
            }

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching payment by ID: {ex.Message}");
        }
    }

}
