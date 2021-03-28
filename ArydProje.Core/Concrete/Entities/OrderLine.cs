using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Concrete.Entities
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual OrderHeader OrderHeader { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
