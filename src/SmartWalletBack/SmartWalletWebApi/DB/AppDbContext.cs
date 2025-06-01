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
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSme> ProductSmes { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<SellersProducts> SellersProducts { get; set; }





    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SellersProducts>()
            .HasKey(sp => sp.Id);

        modelBuilder.Entity<SellersProducts>()
            .HasOne(sp => sp.Seller)
            .WithMany(s => s.SellersProducts)
            .HasForeignKey(sp => sp.SellerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SellersProducts>()
            .HasOne(sp => sp.ProductSme)
            .WithMany(p => p.SellersProducts)
            .HasForeignKey(sp => sp.ProductSmeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
