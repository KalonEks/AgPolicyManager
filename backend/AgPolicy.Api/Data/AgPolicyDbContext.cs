using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Data;

public class AgPolicyDbContext(DbContextOptions<AgPolicyDbContext> options) : DbContext(options)
{
    public DbSet<Farmer> Farmers => Set<Farmer>();
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<CropPolicy> Policies => Set<CropPolicy>();
    public DbSet<Claim> Claims => Set<Claim>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Farms)
            .WithOne(f => f.Farmer)
            .HasForeignKey(f => f.FarmerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Quotes)
            .WithOne(q => q.Farmer)
            .HasForeignKey(q => q.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Policies)
            .WithOne(p => p.Farmer)
            .HasForeignKey(p => p.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Quotes)
            .WithOne(q => q.Farm)
            .HasForeignKey(q => q.FarmId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Policies)
            .WithOne(p => p.Farm)
            .HasForeignKey(p => p.FarmId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CropPolicy>()
            .HasMany(p => p.Claims)
            .WithOne(c => c.Policy)
            .HasForeignKey(c => c.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CropPolicy>()
            .HasOne(p => p.Quote)
            .WithOne()
            .HasForeignKey<CropPolicy>(p => p.QuoteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farmer>().Property(f => f.FirstName).HasMaxLength(100);
        modelBuilder.Entity<Farmer>().Property(f => f.LastName).HasMaxLength(100);
        modelBuilder.Entity<Farmer>().Property(f => f.Email).HasMaxLength(200);
        modelBuilder.Entity<Farm>().Property(f => f.FarmName).HasMaxLength(150);
        modelBuilder.Entity<Quote>().Property(q => q.CropType).HasMaxLength(50);
        modelBuilder.Entity<CropPolicy>().Property(p => p.CropType).HasMaxLength(50);
    }
}
