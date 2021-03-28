using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Models
{
    public class NewOrderViewModel
    {
        public OrderHeaderCreateDto OrderHeaderCreateDto { get; set; }
        public OrderLineCreateDto OrderLineCreateDto { get; set; }
    }
}
