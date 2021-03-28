using ArydProje.Api.Models;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.Api.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderLineUpdateDto, OrderLine>().ReverseMap();
            CreateMap<OrderLineCreateDto, OrderLine>().ReverseMap();
            CreateMap<OrderHeaderCreateDto, OrderHeader>().ReverseMap();
            CreateMap<OrderLine, LineToHeaderDto>().ReverseMap();
            CreateMap<OrderHeader, HeaderWithLinesDto>().ReverseMap();
            CreateMap<OrderLine, ApiUpdateLineDto>().ReverseMap();
            CreateMap<OrderHeader, ApiUpdateHeaderDto>().ReverseMap();
            CreateMap<NewLineModel, OrderLine>().ReverseMap();
            CreateMap<NewHeaderModel, OrderHeader>().ReverseMap();
            CreateMap<ReturnPostHeaderModel, OrderHeader>().ReverseMap();
            CreateMap<ApiCreateLineDto, OrderLine>().ReverseMap();
        }
    }
}
