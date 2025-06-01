namespace SmartWalletWebApi.Models;

public class Product
{
    public int Id { get; set; }
    public int? PaymentId { get; set; }
    public Payment? Payment { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}
