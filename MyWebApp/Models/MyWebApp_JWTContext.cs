using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
 #nullable disable

namespace MyWebApp.Models
{
    public partial class MyWebApp_JWTContext : DbContext
    {
        public MyWebApp_JWTContext()
        {
        }

        public MyWebApp_JWTContext(DbContextOptions<MyWebApp_JWTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=MyWebApp_JWT;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }


        public IQueryable<Products> ShowProducts()
        {
            return this.Products.FromSqlRaw("EXEC Sp_ShowProducts");
        }
        public IQueryable<Products> ShowProductsImage()
        {
            return this.Products.FromSqlRaw("EXEC [Sp_ShowProductsWithImage]");
        }
       
        public void InsertProduct(Products products)
        {
            SqlParameter pcatId = new SqlParameter("@categoryId", products.CategoryId);
            SqlParameter pName = new SqlParameter("@name", products.ProductName);
            SqlParameter qantity = new SqlParameter("@quantity", products.Quantity);
            SqlParameter unitPrice = new SqlParameter("@unitPrice", products.UnitPrice);
            SqlParameter storeDate = new SqlParameter("@storeDate", products.StoreDate);
            SqlParameter isAvailable = new SqlParameter("@isAvailable", products.IsAvailable);
            this.Database.ExecuteSqlCommand("EXEC Sp_ProductInsert @categoryId,@name,@quantity,@unitPrice,@storeDate,@isAvailable", pcatId, pName, qantity, unitPrice, storeDate, isAvailable);
        }
        public void UpdateProduct(Products products)
        {
            SqlParameter id = new SqlParameter("@id", products.Id);
            SqlParameter pcatId = new SqlParameter("@categoryId", products.CategoryId);
            SqlParameter pName = new SqlParameter("@name", products.ProductName);
            SqlParameter qantity = new SqlParameter("@quantity", products.Quantity);
            SqlParameter unitPrice = new SqlParameter("@unitPrice", products.UnitPrice);
            SqlParameter storeDate = new SqlParameter("@storeDate", products.StoreDate);
            SqlParameter isAvailable = new SqlParameter("@isAvailable", products.IsAvailable);
            this.Database.ExecuteSqlCommand("EXEC Sp_ProductUpdate @id,@categoryId,@name,@quantity,@unitPrice,@storeDate,@isAvailable", id, pcatId, pName, qantity, unitPrice, storeDate, isAvailable);
        }
        public void DeleteProduct(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            this.Database.ExecuteSqlCommand("EXEC Sp_DeleteProduct @id", pId);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ProductIm__Produ__29572725");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StoreDate).HasColumnType("date");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Products__Catego__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
