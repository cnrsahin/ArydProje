using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Concrete.Entities
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int VoucherNo { get; set; }
        public Guid SpecialCode { get; set; }
        public string ProjectCode { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }

        public OrderHeader()
        {
            OrderLines = new Collection<OrderLine>();
        }
    }
}
