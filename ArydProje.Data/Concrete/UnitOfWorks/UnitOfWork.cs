using ArydProje.Core.Abstract.Repositories;
using ArydProje.Core.Abstract.UnitOfWorks;
using ArydProje.Data.Concrete.DbContexts;
using ArydProje.Data.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Data.Concrete.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _orderDbContext;
        public UnitOfWork(OrderDbContext context)
        {
            _orderDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        private OrderHeaderRepository _orderHeaderRepository;
        private OrderLineRepository _orderLineRepository;

        public IOrderHeaderRepository OrderHeader => _orderHeaderRepository = _orderHeaderRepository
            ?? new OrderHeaderRepository(_orderDbContext);

        public IOrderLineRepository OrderLine => _orderLineRepository = _orderLineRepository
            ?? new OrderLineRepository(_orderDbContext);

        public void Save()
        {
            _orderDbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
