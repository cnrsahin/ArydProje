using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Profiles
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
        }
    }
}
