﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingProcessingSystem.Models;

namespace OrderingProcessingSystem.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20190808202215_CreateOrdersDB")]
    partial class CreateOrdersDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderingProcessingSystem.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount");

                    b.Property<string>("Artnum");

                    b.Property<string>("Brutprice");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Billingaddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Billcity");

                    b.Property<string>("Billemail");

                    b.Property<string>("Billfname");

                    b.Property<string>("Billstreet");

                    b.Property<string>("Billstreetnr");

                    b.Property<string>("Billzip");

                    b.Property<int>("CountryId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Billingaddresses");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Geo");

                    b.HasKey("Id");

                    b.ToTable("Countrys");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BillingaddressId");

                    b.Property<string>("Orderdate");

                    b.Property<string>("Oxid");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("BillingaddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Orderarticles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArticleId");

                    b.Property<int?>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("OrderId");

                    b.ToTable("Orderarticles");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount");

                    b.Property<string>("Methodname");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Billingaddress", b =>
                {
                    b.HasOne("OrderingProcessingSystem.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Order", b =>
                {
                    b.HasOne("OrderingProcessingSystem.Models.Billingaddress", "Billingaddress")
                        .WithMany()
                        .HasForeignKey("BillingaddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Orderarticles", b =>
                {
                    b.HasOne("OrderingProcessingSystem.Models.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("OrderingProcessingSystem.Models.Order", "Order")
                        .WithMany("Orderarticles")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("OrderingProcessingSystem.Models.Payment", b =>
                {
                    b.HasOne("OrderingProcessingSystem.Models.Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
