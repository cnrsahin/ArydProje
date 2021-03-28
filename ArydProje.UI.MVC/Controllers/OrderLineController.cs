using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using ArydProje.UI.MVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Controllers
{
    public class OrderLineController : Controller
    {
        private readonly IOrderLineService _orderLineService;
        private readonly IMapper _mapper;

        public OrderLineController(IOrderLineService orderLineService, IMapper mapper)
        {
            _orderLineService = orderLineService ?? throw new ArgumentNullException(nameof(orderLineService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int orderHeaderId)
        {
            var result = await _orderLineService.DeleteWithOrderHeaderAsync(id, orderHeaderId);
            if (result.Status == Status.Info)
                return RedirectToAction("GetLines", "Home", new { orderHeaderId });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _orderLineService.GetAsync(i => i.Id == id);

            if (result.Status == Status.Error)
                return RedirectToAction("Index", "Home");

            var model = _mapper.Map<OrderLineUpdateDto>(result.Data);
            return PartialView("_OrderLineUpdatePartialView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderLineUpdateDto orderLineUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var orderLine = _mapper.Map<OrderLine>(orderLineUpdateDto);
                var result = await _orderLineService.OrderLineUpdateWithHeaderAmountAsync(orderLine);

                if (result.Status == Status.Success)
                    return RedirectToAction("GetLines", "Home", new { orderLineUpdateDto.OrderHeaderId });
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Add(int orderHeaderId)
        {
            var model = new OrderLineAddViewModel
            {
                OrderHeaderId = orderHeaderId
            };

            return PartialView("_OrderLineAddPartialView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderLineAddViewModel orderLineAddViewModel)
        {
            orderLineAddViewModel.OrderLineDto.OrderHeaderId = orderLineAddViewModel.OrderHeaderId;
            var orderLine = _mapper.Map<OrderLine>(orderLineAddViewModel.OrderLineDto);

            var result = await _orderLineService.AddOrderLineWithCalculateHeaderAmountAsync(orderLine);

            if (result.Status == Status.Success)
                return RedirectToAction("GetLines", "Home", new { orderLineAddViewModel.OrderHeaderId });

            return RedirectToAction("Index", "Home");
        }
    }
}
