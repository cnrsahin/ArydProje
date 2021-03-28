using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Models
{
    public class OrderLineWithHeaderViewModel
    {
        public IEnumerable<OrderLineDto> OrderLineDtos { get; set; }
        public OrderHeaderDto OrderHeaderDto { get; set; }
    }
}
