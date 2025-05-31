public class Income
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateReceived { get; set; } = DateTime.UtcNow;
    public IncomeType Type { get; set; }
    public required string Currency { get; set; }

    public int UserId { get; set; }
}
