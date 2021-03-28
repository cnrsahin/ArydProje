using ArydProje.Core.Abstract.Repositories;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Data.Concrete.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Data.Concrete.Repositories
{
    public class OrderLineRepository : Repository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(OrderDbContext context) : base(context)
        {
        }

        private OrderDbContext _orderDbContext { get => _context as OrderDbContext; }

        public async Task<IEnumerable<OrderLine>> GetAllLinesByHeaderIdAsync(int orderHeaderId)
        {
            return await _orderDbContext.OrderLines.Where(i => i.OrderHeaderId == orderHeaderId).ToListAsync();
        }
    }
}
