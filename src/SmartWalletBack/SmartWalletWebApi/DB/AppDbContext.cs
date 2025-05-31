using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.Models;

namespace SmartWalletWebApi.DB;

public class AppDbContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
  
}
