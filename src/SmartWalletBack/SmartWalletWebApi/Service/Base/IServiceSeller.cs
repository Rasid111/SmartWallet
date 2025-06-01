using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.Service.Base;

public interface IServiceSeller
{
    Task<IEnumerable<Seller>> GetAllSellersAsync();
    Task<Seller?> GetSellerByIdAsync(int id);
    Task AddSellerAsync(Seller seller);
    Task UpdateSellerAsync(Seller seller);
    Task DeleteSellerAsync(int id);
    Task<IEnumerable<ProductSme>> GetProductsBySellerIdAsync(int sellerId);
    Task<IEnumerable<Seller>> GetSellersByProductIdAsync(int productSmeId);
}
