using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Models;

namespace StoreSystem.DAL.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Item
        //    modelBuilder.Entity<Item>(entity =>
        //    {
        //        entity.HasKey(i => i.Id);

        //        entity.Property(i => i.ItemName)
        //        .HasMaxLength(100)
        //        .IsRequired();

        //        entity.Property(i => i.ItemPrice)
        //        .HasColumnType("decimal(18,2)");

        //        entity.HasIndex(i => i.ItemCode)
        //        .IsUnique();
        //    });

        //    // Purchase -> Item
        //    modelBuilder.Entity<Purchase>(entity =>
        //    {
        //        entity.HasKey(p => p.Id);

        //        entity.HasOne(p => p.Item)
        //        .WithMany(i => i.Purchase)
        //        .HasForeignKey(p => p.ItemId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //        entity.Property(p => p.Quantity)
        //        .IsRequired();
        //    });

        //    // Sell -> Item
        //    modelBuilder.Entity<Sell>(entity =>
        //    {
        //        entity.HasKey(s => s.Id);

        //        entity.HasOne(s => s.Item)
        //        .WithMany(i => i.Sell)
        //        .HasForeignKey(s => s.ItemId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //        entity.Property(s => s.SellingPrice)
        //        .HasColumnType("decimal(18,2)");
        //    });
        //}
    }
}
