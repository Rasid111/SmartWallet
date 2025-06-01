
namespace SmartWalletWebApi.Models;

public class Seller
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<SellersProducts> SellersProducts { get; set; } = new List<SellersProducts>();
}
