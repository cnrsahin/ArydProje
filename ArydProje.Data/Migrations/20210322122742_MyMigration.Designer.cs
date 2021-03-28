﻿// <auto-generated />
using System;
using ArydProje.Data.Concrete.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArydProje.Data.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20210322122742_MyMigration")]
    partial class MyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArydProje.Core.Concrete.Entities.OrderHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectCode")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid>("SpecialCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<int>("VoucherNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderHeader");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2021, 3, 22, 15, 27, 41, 859, DateTimeKind.Local).AddTicks(9070),
                            ProjectCode = "PRJ-MVC",
                            SpecialCode = new Guid("8d5360fc-fbc7-4093-a048-076d8b718d4a"),
                            TotalAmount = 220m,
                            VoucherNo = 1001
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2021, 3, 22, 15, 27, 41, 861, DateTimeKind.Local).AddTicks(2897),
                            ProjectCode = "PRJ-MVC",
                            SpecialCode = new Guid("d6a4d546-722c-4785-8241-5999de7b9225"),
                            TotalAmount = 220m,
                            VoucherNo = 1002
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2021, 3, 22, 15, 27, 41, 861, DateTimeKind.Local).AddTicks(2980),
                            ProjectCode = "PRJ-MVC",
                            SpecialCode = new Guid("8135f597-722f-455a-8b94-b5a3fad4c753"),
                            TotalAmount = 220m,
                            VoucherNo = 1003
                        });
                });

            modelBuilder.Entity("ArydProje.Core.Concrete.Entities.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MaterialCode")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("MaterialDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("OrderLines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 1,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        },
                        new
                        {
                            Id = 2,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 1,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        },
                        new
                        {
                            Id = 3,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 2,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        },
                        new
                        {
                            Id = 4,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 2,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        },
                        new
                        {
                            Id = 5,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 3,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        },
                        new
                        {
                            Id = 6,
                            MaterialCode = "test",
                            MaterialDescription = "test",
                            OrderHeaderId = 3,
                            Quantity = 10m,
                            TaxAmount = 10m,
                            TaxRate = 10m,
                            TotalAmount = 110m,
                            UnitPrice = 10m
                        });
                });

            modelBuilder.Entity("ArydProje.Core.Concrete.Entities.OrderLine", b =>
                {
                    b.HasOne("ArydProje.Core.Concrete.Entities.OrderHeader", "OrderHeader")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("ArydProje.Core.Concrete.Entities.OrderHeader", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
