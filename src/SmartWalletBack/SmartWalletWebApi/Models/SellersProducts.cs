using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWalletWebApi.Models;

public class SellersProducts
{
    public int Id { get; set; }
    
    public int SellerId { get; set; }
    public Seller Seller { get; set; }

    public int ProductSmeId { get; set; }
    public ProductSme ProductSme { get; set; }

    public decimal Price { get; set; }
}

