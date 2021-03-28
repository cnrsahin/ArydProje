using ArydProje.Core.Abstract.Results;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Services
{
    public interface IOrderLineService : IService<OrderLine>
    {
        Task<IDataResult<IEnumerable<OrderLine>>> GetAllLinesByHeaderIdAsync(int orderHeaderId);
        Task<IDataResult<OrderLine>> DeleteWithOrderHeaderAsync(int id, int orderHeaderId);
        Task<IResult> OrderLineUpdateWithHeaderAmountAsync(OrderLine orderLine);
        Task<IResult> AddOrderLineWithCalculateHeaderAmountAsync(OrderLine orderLine);
    }
}
