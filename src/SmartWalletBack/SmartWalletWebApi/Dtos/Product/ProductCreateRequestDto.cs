using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWalletWebApi.Dtos.Product;

public class ProductCreateRequestDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
}
