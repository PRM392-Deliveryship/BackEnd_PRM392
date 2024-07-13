using System;
using System.Collections.Generic;
using GaVietNam_Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GaVietNam_Repository.Entity;

public partial class GaVietNamContext : DbContext
{
    public GaVietNamContext()
    {
    }

    public GaVietNamContext(DbContextOptions<GaVietNamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Chicken> Chickens { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Kind> Kinds { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("MyDB");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Customer" }
        );

        // Seed data for Admins
        //string plaintextPassword = "admin"; // Original password
        //string hashedPassword = HashPassword(plaintextPassword); // Encrypt password using SHA-512
        modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, RoleId = 1, Username = "admin", Password = "1", Status = true }
        );

        // Seed data for Contacts
        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, Phone = "123456789", Email = "contact1@example.com", Facebook = "facebook1", Instagram = "instagram1", Tiktok = "tiktok1", Shoppee = "shoppee1" }
        );

        // Seed data for Kinds
        modelBuilder.Entity<Kind>().Property(k => k.Image).IsRequired(false);
        modelBuilder.Entity<Kind>().HasData(
           new Kind { Id = 1, ChickenId = 1, KindName = "Xào", Image = "image1.jpg", Quantity = 5, Status = true },
           new Kind { Id = 2, ChickenId = 1, KindName = "Luộc", Image = "image2.jpg", Quantity = 5, Status = true },
           new Kind { Id = 3, ChickenId = 2, KindName = "Gỏi", Image = "image1.jpg", Quantity = 5, Status = true },
           new Kind { Id = 4, ChickenId = 2, KindName = "Nướng", Image = "image2.jpg", Quantity = 5, Status = true }
        );

        //Seed data for chicken
        modelBuilder.Entity<Chicken>().HasData(
            new Chicken { Id = 1, Name = "Gà Tam Hoàng", Price = 1000000, Stock = 10, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
            new Chicken { Id = 2, Name = "Gà Ta", Price = 2000000, Stock = 10, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true }
            );

        //Seed data for Order
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, UserId = 1, AdminId = 1, OrderRequirement = "ahihi", OrderCode = "ahihi", PaymentMethod = "ahihi", CreateDate = DateTime.Now, TotalPrice = 1000000, Status = "Successful" }
            );

        //Seed data for OrderItem
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, KindId = 1, OrderId = 1, Quantity = 1, Price = 1000000 }
            );

        //Seed data for User
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, RoleId = 3, Username = "kaneki", Password = "12345", FullName = "Pham Quoc Dat", Email = "phamdat720749pd@gmail.com", Avatar = "https://firebasestorage.googleapis.com/v0/b/GaVietNam-384e4.appspot.com/o/images%2F46822b4c-ad52-49c9-8602-98b1ba92e39c_jingliu-Photoroom.png-Photoroom.png?alt=media&token=277a8993-ec54-4806-9358-de42ae9ce807", Gender = "Male" ,IdentityCard = "074202000730", Dob = DateTime.Now, Phone = "0855720749", CreateDate = DateTime.Now, Status = true}
            );

        //Seed data for Cart
        modelBuilder.Entity<Cart>().HasData(
            new Cart { Id = 1, UserId = 1}
            );

        //Seed data for CartItem
        modelBuilder.Entity<CartItem>().HasData(
            new CartItem { Id = 1, CartId = 1, KindId = 1, Quantity = 1}
            );
    }
}
