using ArydProje.Core.Abstract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IOrderHeaderRepository OrderHeader { get; }
        public IOrderLineRepository OrderLine { get; }

        void Save();
        Task SaveAsync();
    }
}
