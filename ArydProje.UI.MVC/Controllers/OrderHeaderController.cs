using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Controllers
{
    public class OrderHeaderController : Controller
    {
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IMapper _mapper;

        public OrderHeaderController(IOrderHeaderService orderHeaderService, IMapper mapper)
        {
            _orderHeaderService = orderHeaderService ?? throw new ArgumentNullException(nameof(orderHeaderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int orderHeaderId)
        {
            if (orderHeaderId < 1)
                return RedirectToAction("Home", "GetLines");

            var result = await _orderHeaderService.DeleteWithOrderLines(orderHeaderId);
            if (result.Status == Status.Success)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("Home", "GetLines");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _orderHeaderService.GetAsync(i => i.Id == id);

            if (result.Status == Status.Error)
                return RedirectToAction("Index", "Home");

            var model = _mapper.Map<OrderHeaderDto>(result.Data);
            return PartialView("_OrderHeaderUpdatePartialView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderHeaderDto orderHeaderDto)
        {
            if (ModelState.IsValid)
            {
                var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
                var result = await _orderHeaderService.UpdateAsync(orderHeader);

                if (result.Status == Status.Success)
                    return RedirectToAction("GetLines", "Home", new { orderHeaderId = orderHeader.Id });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
