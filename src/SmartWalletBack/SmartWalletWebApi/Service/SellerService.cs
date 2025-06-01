using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Models;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service.Base;

namespace SmartWalletWebApi.Service;

public class SellerService : IServiceSeller
{
    private readonly ISellerRepository sellerRepository;

    public SellerService(ISellerRepository sellerRepository)
    {
        this.sellerRepository = sellerRepository;
    }

    public async Task<IEnumerable<Seller>> GetAllSellersAsync()
    {
        return await sellerRepository.GetAllSellersAsync();
    }

    public async Task<Seller?> GetSellerByIdAsync(int id)
    {
        return await sellerRepository.GetSellerByIdAsync(id);
    }

    public async Task AddSellerAsync(Seller seller)
    {
        await sellerRepository.AddSellerAsync(seller);
    }

    public async Task UpdateSellerAsync(Seller seller)
    {
        await sellerRepository.UpdateSellerAsync(seller);
    }

    public async Task DeleteSellerAsync(int id)
    {
        await sellerRepository.DeleteSellerAsync(id);
    }

    public async Task<IEnumerable<ProductSme>> GetProductsBySellerIdAsync(int sellerId)
    {
        return await sellerRepository.GetProductsBySellerIdAsync(sellerId);
    }

    public async Task<IEnumerable<Seller>> GetSellersByProductIdAsync(int productSmeId)
    {
        return await sellerRepository.GetSellersByProductIdAsync(productSmeId);
    }
}
