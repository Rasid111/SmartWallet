using SmartWalletWebApi.Dtos.Product;
using SmartWalletWebApi.Enums;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.Dtos.Payment;

public class AddPaymentRequestDto
{
    public decimal Amount { get; set; }
    public required string Type { get; set; }
    public string? SallerName { get; set; }

    public IEnumerable<ProductCreateRequestDto> Products { get; set; }

    public int UserId { get; set; }
    public required string Currency { get; set; }
}

