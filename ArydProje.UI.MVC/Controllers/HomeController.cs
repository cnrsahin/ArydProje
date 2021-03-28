using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using ArydProje.UI.MVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IOrderLineService _orderLineService;
        private readonly IMapper _mapper;

        public HomeController(IOrderHeaderService orderHeaderService, IOrderLineService orderLineService, IMapper mapper)
        {
            _orderHeaderService = orderHeaderService ?? throw new ArgumentNullException(nameof(orderHeaderService));
            _orderLineService = orderLineService ?? throw new ArgumentNullException(nameof(orderLineService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLines(int orderHeaderId)
        {
            var orderLinesResult = await _orderLineService.GetAllLinesByHeaderIdAsync(orderHeaderId);
            var orderHeaderResult = await _orderHeaderService.GetAsync(i => i.Id == orderHeaderId);

            if (orderLinesResult.Status == Status.Success && orderHeaderResult.Status == Status.Success)
            {
                var orderLineDtos = _mapper.Map<IEnumerable<OrderLineDto>>(orderLinesResult.Data);
                var orderHeaderDto = _mapper.Map<OrderHeaderDto>(orderHeaderResult.Data);
                var model = new OrderLineWithHeaderViewModel
                {
                    OrderLineDtos = orderLineDtos.OrderByDescending(i => i.Id),
                    OrderHeaderDto = orderHeaderDto
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewOrderViewModel newOrderViewModel)
        {
            if (newOrderViewModel is null)
                return View();

            var newOrderHeader = _mapper.Map<OrderHeader>(newOrderViewModel.OrderHeaderCreateDto);
            var newOrderLine = _mapper.Map<OrderLine>(newOrderViewModel.OrderLineCreateDto);

            var result = await _orderHeaderService.CreateOrderAsync(newOrderHeader, newOrderLine);

            if (result.Status == Status.Success)
                return RedirectToAction("GetLines", "Home", new { orderHeaderId = result.Data.Id });

            return View();
        }
    }
}
