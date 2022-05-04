using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Mocks;
using MVCProject2.Data.Models;

namespace MVCProject2.Data
{
    public class AppDBContent : DbContext
    {
        private readonly IProductsCategory _productsCategory = new MockCategory();
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.Provider).HasMaxLength(250);
                entity.Property(e => e.NameIdentifier).HasMaxLength(500);
                entity.Property(e => e.Username).HasMaxLength(250);
                entity.Property(e => e.Password).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Firstname).HasMaxLength(250);
                entity.Property(e => e.Lastname).HasMaxLength(250);
                entity.Property(e => e.Mobile).HasMaxLength(250);
                entity.Property(e => e.Roles).HasMaxLength(1000);

                entity.HasData(new AppUser
                {
                    Provider = "Cookies",
                    UserId = 1,
                    NameIdentifier = "my main account",
                    Email = "bob@admonex.com",
                    Username = "bob",
                    Password = "pizza",
                    Firstname = "Bob",
                    Lastname = "Tester",
                    Mobile = "800-555-1212",
                    Roles = "Admin"
                });

            });
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Category> category { get; set; }

        public DbSet<ShopProductItem> shopProductItem { get; set; }

        public DbSet<Order> order { get; set; }

        public DbSet<OrderDetail> orderDetail { get; set; }

    }
}
