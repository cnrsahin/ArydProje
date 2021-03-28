using ArydProje.Core.Abstract.Calculator;
using ArydProje.Core.Abstract.Repositories;
using ArydProje.Core.Abstract.Results;
using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Abstract.UnitOfWorks;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Business.Concrete
{
    public class OrderLineService : Service<OrderLine>, IOrderLineService
    {
        private readonly ICalculator _calculator;
        public OrderLineService(IUnitOfWork unitOfWork, IRepository<OrderLine> repository, ICalculator calculator)
            : base(unitOfWork, repository)
        {
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public async Task<IDataResult<OrderLine>> DeleteWithOrderHeaderAsync(int id, int orderHeaderId)
        {
            var lineEntity = await _unitOfWork.OrderLine.GetAsync(i => i.Id == id);
            if (lineEntity == null)
                return new DataResult<OrderLine>(Status.Error);

            var lineEntityAmount = lineEntity.TotalAmount;
            await _unitOfWork.OrderLine.DeleteAsync(lineEntity);

            var headerEntity = await _unitOfWork.OrderHeader.GetAsync(i => i.Id == orderHeaderId);
            headerEntity.TotalAmount -= lineEntityAmount;

            if (headerEntity.TotalAmount <= 0)
            {
                await _unitOfWork.OrderHeader.DeleteAsync(headerEntity);
                await _unitOfWork.SaveAsync();
                return new DataResult<OrderLine>(Status.Success);
            }
            else
            {
                await _unitOfWork.OrderHeader.UpdateAsync(headerEntity);
                await _unitOfWork.SaveAsync();
                return new DataResult<OrderLine>(Status.Info);
            }
        }

        public async Task<IDataResult<IEnumerable<OrderLine>>> GetAllLinesByHeaderIdAsync(int orderHeaderId)
        {
            if (orderHeaderId < 1)
                return new DataResult<IEnumerable<OrderLine>>(Status.Error);
            else
            {
                var result = await _unitOfWork.OrderLine.GetAllLinesByHeaderIdAsync(orderHeaderId);
                return new DataResult<IEnumerable<OrderLine>>(Status.Success, result);
            }
        }

        public async Task<IResult> OrderLineUpdateWithHeaderAmountAsync(OrderLine orderLine)
        {
            if (orderLine is null)
                return new Result(Status.Error);

            var oldAmountLine = orderLine.TotalAmount;
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(i => i.Id == orderLine.OrderHeaderId);
            bool isLine = await _unitOfWork.OrderLine.AnyAsync(i => i.Id == orderLine.Id);

            if (orderHeader != null && isLine)
            {
                var newOrderLine = _calculator.TaxCalculate(orderLine);
                orderHeader.TotalAmount = orderHeader.TotalAmount - oldAmountLine + newOrderLine.TotalAmount;
                await _unitOfWork.OrderLine.UpdateAsync(newOrderLine);
                await _unitOfWork.OrderHeader.UpdateAsync(orderHeader);
                await _unitOfWork.SaveAsync();

                return new Result(Status.Success);
            }
            else
                return new Result(Status.Error);
        }

        public async Task<IResult> AddOrderLineWithCalculateHeaderAmountAsync(OrderLine orderLine)
        {
            if (orderLine is null)
                return new Result(Status.Error);

            var entity = _calculator.TaxCalculate(orderLine);

            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(i => i.Id == entity.OrderHeaderId);
            orderHeader.TotalAmount += entity.TotalAmount;

            _unitOfWork.OrderLine.Add(entity);
            await _unitOfWork.OrderHeader.UpdateAsync(orderHeader);
            await _unitOfWork.SaveAsync();

            return new Result(Status.Success);
        }
    }
}
