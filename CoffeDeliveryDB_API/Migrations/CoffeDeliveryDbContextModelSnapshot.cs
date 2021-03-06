// <auto-generated />
using System;
using CoffeDeliveryDB_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoffeDeliveryDB_API.Migrations
{
    [DbContext(typeof(CoffeDeliveryDbContext))]
    partial class CoffeDeliveryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Russian_Kazakhstan.1251")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.HasSequence("seq_dishes");

            modelBuilder.HasSequence("seq_orders");

            modelBuilder.Entity("CoffeDeliveryDB_API.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('seq_dishes'::regclass)");

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("character varying")
                        .HasColumnName("picture_url");

                    b.Property<int?>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("dishes");
                });

            modelBuilder.Entity("CoffeDeliveryDB_API.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('seq_orders'::regclass)");

                    b.Property<string>("Address")
                        .HasColumnType("character varying")
                        .HasColumnName("address");

                    b.Property<string>("CookerId")
                        .HasColumnType("character varying")
                        .HasColumnName("cooker_id");

                    b.Property<string>("CourierId")
                        .HasColumnType("character varying")
                        .HasColumnName("courier_id");

                    b.Property<DateTime?>("Datetimeordercooked")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datetimeordercooked");

                    b.Property<DateTime?>("Datetimeorderdelivered")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datetimeorderdelivered");

                    b.Property<DateTime?>("Datetimeorderplaced")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datetimeorderplaced");

                    b.Property<int?>("DishId")
                        .HasColumnType("integer")
                        .HasColumnName("dish_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("CoffeDeliveryDB_API.Order", b =>
                {
                    b.HasOne("CoffeDeliveryDB_API.Dish", "Dish")
                        .WithMany("Orders")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_dish_id");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("CoffeDeliveryDB_API.Dish", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
