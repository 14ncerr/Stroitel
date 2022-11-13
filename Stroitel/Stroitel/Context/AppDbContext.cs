using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Stroitel.Models;

namespace Stroitel.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<PickupPoint> PickupPoints { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Provider> Providers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GF15ASA\\SQLEXPRESS; Database=StroitelDb; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderPickupCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.OrderPickupPoint)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderPickupPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__OrderPick__2B3F6F97");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                    .HasName("PK__OrderPro__817A266280769ACD");

                entity.ToTable("OrderProduct");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductArticleNumber).HasMaxLength(100);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Order__38996AB5");

                entity.HasOne(d => d.ProductArticleNumberNavigation)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductArticleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__398D8EEE");
            });

            modelBuilder.Entity<PickupPoint>(entity =>
            {
                entity.ToTable("PickupPoint");

                entity.Property(e => e.PickupPointId).HasColumnName("PickupPointID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductArticleNumber)
                    .HasName("PK__Product__2EA7DCD528DAA6FE");

                entity.ToTable("Product");

                entity.Property(e => e.ProductArticleNumber).HasMaxLength(100);

                entity.Property(e => e.ProductCost).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.ProudctUnit).HasMaxLength(24);

                entity.HasOne(d => d.ProductCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__33D4B598");

                entity.HasOne(d => d.ProductManufacturerNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductManufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__35BCFE0A");

                entity.HasOne(d => d.ProductProviderNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductProvider)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__34C8D9D1");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("Provider");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserPatronymic).HasMaxLength(100);

                entity.Property(e => e.UserSurname).HasMaxLength(100);

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__UserRole__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
