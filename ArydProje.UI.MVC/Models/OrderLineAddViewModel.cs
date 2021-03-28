using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.UI.MVC.Models
{
    public class OrderLineAddViewModel
    {
        public int OrderHeaderId { get; set; }
        public OrderLineDto OrderLineDto { get; set; }
    }
}
