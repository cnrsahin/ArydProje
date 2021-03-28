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
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArydProje.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeadersController : ControllerBase
    {
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IMapper _mapper;

        public OrderHeadersController(IOrderHeaderService orderHeaderService, IMapper mapper)
        {
            _orderHeaderService = orderHeaderService ?? throw new ArgumentNullException(nameof(orderHeaderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _orderHeaderService.GetAllAsync();

            if (result.Status == Status.Error || result.Data.Count() == 0)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Herhangi bir veri bulunmamaktadir!"
                }));

            var model = JsonSerializer.Serialize(_mapper.Map<IEnumerable<OrderHeaderDto>>(result.Data));

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

            var result = await _orderHeaderService.GetAsync(i => i.Id == id);

            if (result.Status == Status.Error)
            {
                var errorModel = JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                });

                return BadRequest(errorModel);
            }

            var model = JsonSerializer.Serialize(_mapper.Map<OrderHeaderDto>(result.Data));

            return Ok(model);
        }

        [HttpGet("{id}/orderLines")]
        public async Task<IActionResult> GetOrderHeaderWithLinesByIdAsync(int id)
        {
            if (id < 1)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali id girisi, lutfen 0 dan buyuk bir id giriniz!"
                }));

            var result = await _orderHeaderService.GetOrderHeaderWithLinesByIdAsync(id);

            if (result.Status == Status.Error)
            {
                var errorModel = JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                });

                return BadRequest(errorModel);
            }

            var model = JsonSerializer.Serialize(new HeaderWithLinesDto
            {
                OrderLines = _mapper.Map<HeaderWithLinesDto>(result.Data).OrderLines,
                 OrderHeader = _mapper.Map<OrderHeaderDto>(result.Data)
            });

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

            var result = await _orderHeaderService.DeleteWithOrderLines(id);

            if (result.Status == Status.Error)
            {
                var errorModel = JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Veritabaninda bu idye sahip veri bulunamadi!"
                });

                return BadRequest(errorModel);
            }

            return Ok(new { Message = "Idsi girilen veri basariyla silindi!" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ApiUpdateHeaderDto apiUpdateHeaderDto)
        {
            var isHeader = await _orderHeaderService.AnyAsync(i => i.Id == apiUpdateHeaderDto.Id);

            if (apiUpdateHeaderDto is null || apiUpdateHeaderDto.Id <= 0 || !isHeader.Data)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali veri girisi!"
                }));
            else
            {
                var oldHeader = await _orderHeaderService.GetAsync(i => i.Id == apiUpdateHeaderDto.Id);
                var orderHeader = _mapper.Map<OrderHeader>(apiUpdateHeaderDto);
                orderHeader.Date = oldHeader.Data.Date;
                orderHeader.SpecialCode = oldHeader.Data.SpecialCode;
                orderHeader.VoucherNo = oldHeader.Data.VoucherNo;
                orderHeader.TotalAmount = oldHeader.Data.TotalAmount;
                var result = await _orderHeaderService.UpdateAsync(orderHeader);

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
        public async Task<IActionResult> CreateOrderAsync(ApiCreateOrderModel apiCreateOrderModel)
        {
            if (apiCreateOrderModel is null)
                return BadRequest(JsonSerializer.Serialize(new ErrorModel
                {
                    Message = "Hatali veri girisi!"
                }));
            else
            {
                var newHeader = _mapper.Map<OrderHeader>(apiCreateOrderModel.NewHeaderModel);
                var newLine = _mapper.Map<OrderLine>(apiCreateOrderModel.NewLineModel);

                var result = await _orderHeaderService.CreateOrderAsync(newHeader, newLine);

                if (result.Status == Status.Error)
                {
                    var errorModel = JsonSerializer.Serialize(new ErrorModel
                    {
                        Message = "Veritabanina kayit yapilamadi!"
                    });

                    return BadRequest(errorModel);
                }
                var model = JsonSerializer.Serialize(_mapper.Map<ReturnPostHeaderModel>(result.Data));

                return Created("", model);
            }
        }
    }
}
