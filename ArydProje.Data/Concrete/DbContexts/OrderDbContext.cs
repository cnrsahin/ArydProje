using ArydProje.Core.Concrete.Entities;
using ArydProje.Data.Concrete.Confs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Data.Concrete.DbContexts
{
    public class OrderDbContext : DbContext
    {
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderHeaderConfs());
            modelBuilder.ApplyConfiguration(new OrderLineConfs());
        }
    }
}
