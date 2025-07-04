﻿// <auto-generated />
using System;
using LedMagazineBack.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LedMagazineBack.Migrations
{
    [DbContext(typeof(MagazineDbContext))]
    [Migration("20250701085924_main_migration")]
    partial class main_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LedMagazineBack.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlogId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Guest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GuestId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<long>("OrderNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("OrderNumber"));

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GuestId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.RentTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CartItemId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndOfRentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("OrderItemId")
                        .HasColumnType("uuid");

                    b.Property<byte>("RentMonths")
                        .HasColumnType("smallint");

                    b.Property<byte>("RentSeconds")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CartItemId")
                        .IsUnique();

                    b.HasIndex("OrderItemId")
                        .IsUnique();

                    b.ToTable("RentTimes");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.RentTimeMultiplayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("MonthsDifferenceMultiplayer")
                        .HasColumnType("real");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<float>("SecondsDifferenceMultiplayer")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("RentTimesMultiplayer");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.ScreenSpecifications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ScreenResolution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ScreenSize")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ScreenType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ScreenSpecifications");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Article", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Blog", null)
                        .WithMany("Articles")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Cart", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Customer", null)
                        .WithOne("Cart")
                        .HasForeignKey("LedMagazineBack.Entities.Cart", "CustomerId");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.CartItem", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Guest", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Location", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Product", null)
                        .WithOne("Location")
                        .HasForeignKey("LedMagazineBack.Entities.Location", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Order", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("LedMagazineBack.Entities.Guest", null)
                        .WithMany("Orders")
                        .HasForeignKey("GuestId");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.OrderItem", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.RentTime", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.CartItem", null)
                        .WithOne("RentTime")
                        .HasForeignKey("LedMagazineBack.Entities.RentTime", "CartItemId");

                    b.HasOne("LedMagazineBack.Entities.OrderItem", null)
                        .WithOne("RentTime")
                        .HasForeignKey("LedMagazineBack.Entities.RentTime", "OrderItemId");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.RentTimeMultiplayer", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Product", null)
                        .WithOne("RentTimeMultiplayer")
                        .HasForeignKey("LedMagazineBack.Entities.RentTimeMultiplayer", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.ScreenSpecifications", b =>
                {
                    b.HasOne("LedMagazineBack.Entities.Product", null)
                        .WithOne("ScreenSpecifications")
                        .HasForeignKey("LedMagazineBack.Entities.ScreenSpecifications", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Blog", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.CartItem", b =>
                {
                    b.Navigation("RentTime")
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Customer", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Guest", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LedMagazineBack.Entities.OrderItem", b =>
                {
                    b.Navigation("RentTime")
                        .IsRequired();
                });

            modelBuilder.Entity("LedMagazineBack.Entities.Product", b =>
                {
                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("RentTimeMultiplayer");

                    b.Navigation("ScreenSpecifications");
                });
#pragma warning restore 612, 618
        }
    }
}
