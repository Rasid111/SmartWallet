namespace SmartWalletWebApi.Dtos.Income;
public class GetIncomeResponseDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateReceived { get; set; }
    public required string Type { get; set; }
    public int UserId { get; set; }
    public required string Currency { get; set; }
}
