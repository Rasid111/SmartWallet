using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;

namespace SmartWalletWebApi.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly AppDbContext context;

    public SellerRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task AddSellerAsync(Seller seller)
    {
        await context.Sellers.AddAsync(seller);
        await context.SaveChangesAsync();
    }

    public async Task DeleteSellerAsync(int id)
    {
        var seller = await context.Sellers.FindAsync(id);
        if (seller != null)
        {
            context.Sellers.Remove(seller);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Seller>> GetAllSellersAsync()
    {
        return await context.Sellers
            .Include(s => s.SellersProducts)
            .ThenInclude(sp => sp.ProductSme)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductSme>> GetProductsBySellerIdAsync(int sellerId)
    {
        return await context.SellersProducts
            .Where(sp => sp.SellerId == sellerId)
            .Include(sp => sp.ProductSme)
            .Select(sp => sp.ProductSme)
            .ToListAsync();
    }

    public async Task<Seller?> GetSellerByIdAsync(int id)
    {
        return await context.Sellers
            .Include(s => s.SellersProducts)
            .ThenInclude(sp => sp.ProductSme)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Seller>> GetSellersByProductIdAsync(int productSmeId)
    {
        return await context.SellersProducts
            .Where(sp => sp.ProductSmeId == productSmeId)
            .Include(sp => sp.Seller)
            .Select(sp => sp.Seller)
            .ToListAsync();
    }

    public async Task UpdateSellerAsync(Seller seller)
    {
        context.Sellers.Update(seller);
        await context.SaveChangesAsync();
    }
}
