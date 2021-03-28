using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Abstract.UnitOfWorks;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using ArydProje.UI.MVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.ViewComponents
{
    public class OrderHeadersViewComponent : ViewComponent
    {
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IMapper _mapper;

        public OrderHeadersViewComponent(IOrderHeaderService orderHeaderService, IMapper mapper)
        {
            _orderHeaderService = orderHeaderService ?? throw new ArgumentNullException(nameof(orderHeaderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _orderHeaderService.GetAllAsync();
            if (result.Status == Status.Success)
            {
                var orderHeaderDtos = _mapper.Map<IEnumerable<OrderHeaderDto>>(result.Data);

                var model = new LeftSideBarViewModel
                {
                    OrderHeaderDtos = orderHeaderDtos.OrderByDescending(i => i.Id),
                    ThisHeader = Convert.ToInt32(HttpContext.Request.Query["orderHeaderId"])
                };

                return View(model);
            }

            return View();
        }
    }
}
