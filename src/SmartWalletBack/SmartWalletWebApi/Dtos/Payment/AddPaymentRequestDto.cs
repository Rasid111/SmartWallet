using SmartWalletWebApi.Enums;

namespace SmartWalletWebApi.Dtos.Payment;

public class AddPaymentRequestDto
{
    public decimal Amount { get; set; }
    public required string Type { get; set; }
    public int UserId { get; set; }
    public required string Currency { get; set; }
}

