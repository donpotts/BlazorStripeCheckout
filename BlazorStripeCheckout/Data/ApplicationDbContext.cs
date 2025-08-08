using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorStripeCheckout.Models;
using BlazorStripeCheckout.Shared.Models;

namespace BlazorStripeCheckout.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Products> Products => Set<Products>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Products>()
            .Property(e => e.Price)
            .HasConversion<double>();
        modelBuilder.Entity<Products>()
            .Property(e => e.Price)
            .HasPrecision(19, 4);
    }
}
