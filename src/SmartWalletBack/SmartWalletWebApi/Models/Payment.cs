using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Enums;

namespace SmartWalletWebApi.Models;

public class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }
    public string? SallerName { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public PaymentType Type { get; set; }
    
    // public IEnumerable<Product> Products { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public int UserId { get; set; }
    public required string Currency { get; set; }
}
