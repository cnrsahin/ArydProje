using ArydProje.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Repositories
{
    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        Task<IEnumerable<OrderLine>> GetAllLinesByHeaderIdAsync(int orderHeaderId);
    }
}
