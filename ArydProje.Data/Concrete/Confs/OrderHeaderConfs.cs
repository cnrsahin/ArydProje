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
    public class OrderHeaderConfs : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Date).IsRequired();
            builder.Property(i => i.VoucherNo).IsRequired();
            builder.Property(i => i.SpecialCode).IsRequired();
            builder.Property(i => i.ProjectCode).IsRequired().HasMaxLength(25);
            builder.Property(i => i.TotalAmount).IsRequired().HasColumnType("decimal(12,2)");
            builder.ToTable("OrderHeader");

            builder.HasData(
                new OrderHeader
                {
                    Id = 1,
                    VoucherNo = 1001,
                    SpecialCode = Guid.NewGuid(),
                    ProjectCode = "PRJ-MVC",
                    Date = DateTime.Now,
                    TotalAmount = 220M
                },
                new OrderHeader
                {
                    Id = 2,
                    VoucherNo = 1002,
                    SpecialCode = Guid.NewGuid(),
                    ProjectCode = "PRJ-MVC",
                    Date = DateTime.Now,
                    TotalAmount = 220M
                },
                new OrderHeader
                {
                    Id = 3,
                    VoucherNo = 1003,
                    SpecialCode = Guid.NewGuid(),
                    ProjectCode = "PRJ-MVC",
                    Date = DateTime.Now,
                    TotalAmount = 220M
                }
                );
        }
    }
}
