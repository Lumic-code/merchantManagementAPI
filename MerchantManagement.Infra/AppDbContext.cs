using MerchantManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MerchantManagement.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Merchant> Merchants => Set<Merchant>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
