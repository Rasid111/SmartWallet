using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartWalletWebApi.Enums;

namespace SmartWalletWebApi.Models;

public class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public PaymentType Type { get; set; }

    public int UserId { get; set; }
    public required string Currency { get; set; }


}

