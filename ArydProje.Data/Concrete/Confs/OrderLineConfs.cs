using ArydProje.Core.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Data.Concrete.Confs
{
    public class OrderLineConfs : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.MaterialCode).IsRequired().HasMaxLength(55);
            builder.Property(i => i.MaterialDescription).IsRequired().HasMaxLength(250);
            builder.Property(i => i.Quantity).IsRequired().HasColumnType("decimal(12,2)");
            builder.Property(i => i.UnitPrice).IsRequired().HasColumnType("decimal(12,2)");
            builder.Property(i => i.TaxRate).IsRequired().HasColumnType("decimal(12,2)");
            builder.Property(i => i.TaxAmount).IsRequired().HasColumnType("decimal(12,2)");
            builder.Property(i => i.TotalAmount).IsRequired().HasColumnType("decimal(12,2)");

            builder.HasOne<OrderHeader>(l => l.OrderHeader).WithMany(h => h.OrderLines).HasForeignKey(l => l.OrderHeaderId);

            builder.HasData(
                new OrderLine   
                {
                    Id = 1,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 1
                },
                new OrderLine
                {
                    Id = 2,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 1
                },
                new OrderLine
                {
                    Id = 3,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 2
                },
                new OrderLine
                {
                    Id = 4,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 2
                },
                new OrderLine
                {
                    Id = 5,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 3
                }
                ,
                new OrderLine
                {
                    Id = 6,
                    MaterialCode = "test",
                    MaterialDescription = "test",
                    Quantity = 10,
                    UnitPrice = 10,
                    TaxRate = 10,
                    TaxAmount = 10,
                    TotalAmount = 110M,
                    OrderHeaderId = 3
                }
                );
        }
    }
}
