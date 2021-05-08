using Faregosoft.NewApi.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faregosoft.NewApi.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Providers> Providers { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
            
            //como yo hago un indice complejo, osea mas de un campo
            //modelBuilder.Entity<Department>(dep =>
            //{
            //    dep.HasIndex("Name", "CountryId").IsUnique();
            //    dep.HasOne(d => d.Country).WithMany(c => c.Departments).OnDelete(DeleteBehavior.Cascade);
            //});
        }
    }
}