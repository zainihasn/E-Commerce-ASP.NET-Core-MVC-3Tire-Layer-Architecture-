using System.Collections.Generic;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static DataAccessLayer.Model.Invoice;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<GuestInfo> GuestInfo { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var adminRoleId = "d59319d9-4f1f-48eb-b36d-1289374944f3";
            var adminUserId = "9274a1e0-9442-4312-80c4-033a07823730";

         
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();

           
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(), 
                ConcurrencyStamp = adminUserId,
                PasswordHash = hasher.HashPassword(null, "Admin123!")
            };

            builder.Entity<IdentityUser>().HasData(adminUser);

    
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );

            base.OnModelCreating(builder);
        }


    }

}
