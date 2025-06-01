using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartWalletWebApi.Dtos.Payment;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Service;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SellerController : ControllerBase
{
    private readonly IServiceSeller sellerService;

    public SellerController(IServiceSeller sellerService)
    {
        this.sellerService = sellerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSellers()
    {
        try
        {
            var payments = await sellerService.GetAllSellersAsync();
            return Ok(payments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching sellers: {ex.Message}");
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetSellerById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid seller ID. It must be greater than zero.");
        }

        try
        {
            var seller = await sellerService.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return NotFound($"Seller with ID {id} not found.");
            }

            return Ok(seller);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching seller by ID: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeller([FromBody] Seller seller)
    {
        if (seller == null)
        {
            return BadRequest("Seller data is missing in request body.");
        }

        try
        {
            await sellerService.AddSellerAsync(seller);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating seller: {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetSellersByProductIdAsync(int productSmeId)
    {
        if (productSmeId <= 0)
        {
            return BadRequest("Invalid productSME ID. It must be greater than zero.");
        }

        try
        {
            var productSME = await sellerService.GetProductsBySellerIdAsync(productSmeId);
            if (productSME == null)
            {
                return NotFound($"productSME with ID {productSmeId} not found.");
            }

            return Ok(productSME);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching productSME by ID: {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetProductsBySellerIdAsync(int sellerId)
    {
        if (sellerId <= 0)
        {
            return BadRequest("Invalid seller ID. It must be greater than zero.");
        }

        try
        {
            var seller = await sellerService.GetProductsBySellerIdAsync(sellerId);
            if (seller == null)
            {
                return NotFound($"Product with ID {sellerId} not found.");
            }

            return Ok(seller);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching seller by ID: {ex.Message}");
        }
    }
}
