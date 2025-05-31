using SmartWalletWebApi.Enums;

namespace SmartWalletWebApi.Dtos.Payment;

public class GetPaymentResponseDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public required string Type { get; set; }
    public int UserId { get; set; }
    public required string Currency { get; set; }
}