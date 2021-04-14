using System;
using CoffeDeliveryDB_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CoffeDeliveryDB_API
{
    public partial class CoffeDeliveryDbContext : DbContext
    {
        public CoffeDeliveryDbContext(DbContextOptions<CoffeDeliveryDbContext> options )
            : base(options)
        {   
        }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Kazakhstan.1251");
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("dishes");
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('seq_dishes'::regclass)");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
                entity.Property(e => e.PictureUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("picture_url");
                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('seq_orders'::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.CookerId)
                    .HasColumnType("character varying")
                    .HasColumnName("cooker_id");

                entity.Property(e => e.CourierId)
                    .HasColumnType("character varying")
                    .HasColumnName("courier_id");

                entity.Property(e => e.Datetimeordercooked).HasColumnName("datetimeordercooked");

                entity.Property(e => e.Datetimeorderdelivered).HasColumnName("datetimeorderdelivered");

                entity.Property(e => e.Datetimeorderplaced).HasColumnName("datetimeorderplaced");

                entity.Property(e => e.DishId).HasColumnName("dish_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_dish_id");
            });

            modelBuilder.HasSequence("seq_dishes");

            modelBuilder.HasSequence("seq_orders");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
