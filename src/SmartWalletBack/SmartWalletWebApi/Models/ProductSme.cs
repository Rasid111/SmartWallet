
namespace SmartWalletWebApi.Models;

public class ProductSme
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<SellersProducts> SellersProducts { get; set; } = new List<SellersProducts>();
}

