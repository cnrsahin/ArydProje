using ArydProje.Core.Abstract.Results;
using ArydProje.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Services
{
    public interface IOrderHeaderService : IService<OrderHeader>
    {
        Task<IDataResult<OrderHeader>> DeleteWithOrderLines(int orderHeaderId);
        Task<IDataResult<OrderHeader>> CreateOrderAsync(OrderHeader orderHeader, OrderLine orderLine);
        Task<IDataResult<OrderHeader>> GetOrderHeaderWithLinesByIdAsync(int orderHeaderID);
    }
}
