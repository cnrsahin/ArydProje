using ArydProje.Core.Abstract.Calculator;
using ArydProje.Core.Abstract.Repositories;
using ArydProje.Core.Abstract.Results;
using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Abstract.UnitOfWorks;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Business.Concrete
{
    public class OrderHeaderService : Service<OrderHeader>, IOrderHeaderService
    {
        private readonly ICalculator _calculator;

        public OrderHeaderService(IUnitOfWork unitOfWork, IRepository<OrderHeader> repository, ICalculator calculator) :
            base(unitOfWork, repository)
        {
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public async Task<IDataResult<OrderHeader>> DeleteWithOrderLines(int orderHeaderId)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(i => i.Id == orderHeaderId, i => i.OrderLines);

            if (orderHeader == null)
                return new DataResult<OrderHeader>(Status.Error);

            foreach (var orderLine in orderHeader.OrderLines)
            {
                await _unitOfWork.OrderLine.DeleteAsync(orderLine);
            }

            await _unitOfWork.OrderHeader.DeleteAsync(orderHeader);
            await _unitOfWork.SaveAsync();

            return new DataResult<OrderHeader>(Status.Success);
        }

        public async Task<IDataResult<OrderHeader>> CreateOrderAsync(OrderHeader orderHeader, OrderLine orderLine)
        {
            if (orderHeader is null || orderLine is null)
                return new DataResult<OrderHeader>(Status.Error);

            var newLine = _calculator.TaxCalculate(orderLine);
            var entity =  _unitOfWork.OrderHeader.Add(orderHeader);
            _unitOfWork.Save();
            newLine.OrderHeaderId = entity.Id;
            entity.TotalAmount = newLine.TotalAmount;
            _unitOfWork.OrderLine.Add(newLine);
            entity.VoucherNo = entity.Id + 1000;
            entity.Date = DateTime.Now;
            entity.SpecialCode = Guid.NewGuid();
            await _unitOfWork.OrderHeader.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new DataResult<OrderHeader>(Status.Success, orderHeader);
        }

        public async Task<IDataResult<OrderHeader>> GetOrderHeaderWithLinesByIdAsync(int orderHeaderID)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(i => i.Id == orderHeaderID);

            if (orderHeader == null)
                return new DataResult<OrderHeader>(Status.Error);
            else
            {
                var orderHeaderWithLines = await _unitOfWork.OrderHeader.GetOrderHeaderWithLinesByIdAsync(orderHeaderID);

                if (orderHeaderWithLines == null)
                    return new DataResult<OrderHeader>(Status.Error);

                return new DataResult<OrderHeader>(Status.Success, orderHeaderWithLines);
            }
        }
    }
}
