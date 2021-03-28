using ArydProje.Api.Models;
using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Concrete.Results;
using ArydProje.Core.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArydProje.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;
        private readonly IMapper _mapper;

        public OrderLinesController(IOrderLineService orderLineService, IMapper mapper)
        {
            _orderLineService = orderLineService ?? throw new ArgumentNullException(nameof(orderLineService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _orderLineService.GetAllAsync();

            if (result.Status == Status.Error || result.Data.Count() == 0)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Herhangi bir veri bulunmamaktadir!"
                }));

            var model = JsonSerializer.Serialize(_mapper.Map<IEnumerable<OrderLineDto>>(result.Data));

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali id girisi, lutfen 0 dan buyuk bir id giriniz!"
                }));

            var result = await _orderLineService.GetAsync(i => i.Id == id);

            if (result.Status == Status.Error)
            {
                var errorModel = JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                });

                return BadRequest(errorModel);
            }

            var model = JsonSerializer.Serialize(_mapper.Map<OrderLineDto>(result.Data));

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali id girisi, lutfen 0 dan buyuk bir id giriniz!"
                }));

            var orderLineHeader = await _orderLineService.GetAsync(i => i.Id == id);

            if (orderLineHeader.Status == Status.Error)
            {
                var errorModel = JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                });

                return BadRequest(errorModel);
            }

            var result = await _orderLineService.DeleteWithOrderHeaderAsync(id, orderLineHeader.Data.OrderHeaderId);

            return Ok(new { Message = "Idsi girilen veri basariyla silindi!" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ApiUpdateLineDto apiUpdateLineDto)
        {
            var isLine = await _orderLineService.AnyAsync(i => i.Id == apiUpdateLineDto.Id);

            if (apiUpdateLineDto is null || apiUpdateLineDto.Id <= 0 || apiUpdateLineDto.OrderHeaderId <= 0 || !isLine.Data)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali veri girisi!"
                }));
            else
            {
                var oldEntity = await _orderLineService.GetAsync(i => i.Id == apiUpdateLineDto.Id);
                var updateLine = _mapper.Map<OrderLine>(apiUpdateLineDto);
                updateLine.TaxAmount = oldEntity.Data.TaxAmount;
                updateLine.TotalAmount = oldEntity.Data.TotalAmount;
                var result = await _orderLineService.OrderLineUpdateWithHeaderAmountAsync(updateLine);

                if (result.Status == Status.Error)
                {
                    var errorModel = JsonSerializer.Serialize(new ErrorModel
                    {
                        Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                    });

                    return BadRequest(errorModel);
                }

                return Ok(new { Message = "Guncelleme islemi basarili!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLine(ApiCreateLineDto apiCreateLineDto)
        {
            if (apiCreateLineDto is null || apiCreateLineDto.OrderHeaderId <= 0)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali veri girisi!"
                }));
            else
            {
                var newLine = _mapper.Map<OrderLine>(apiCreateLineDto);
                var result = await _orderLineService.AddOrderLineWithCalculateHeaderAmountAsync(newLine);

                if (result.Status == Status.Error)
                {
                    var errorModel = JsonSerializer.Serialize(new ErrorModel
                    {
                        Message = "Veritabanina kayit yapilamadi!"
                    });

                    return BadRequest(errorModel);
                }

                return Created("", new { Message = "Malzeme kaydi yapildi!" });
            }
        }
    }
}
