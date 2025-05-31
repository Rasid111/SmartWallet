namespace SmartWalletWebApi.Dtos.Income;
public class AddIncomeRequestDto
{
    public decimal Amount { get; set; }
    public required string Type { get; set; }
    public int UserId { get; set; }
    public required string Currency { get; set; }
}
