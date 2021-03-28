using ArydProje.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Dtos
{
    public class LineToHeaderDto
    {
        public int Id { get; set; }

        [DisplayName("Malzeme Kodu")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(55, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string MaterialCode { get; set; }

        [DisplayName("Malzeme Açıklaması")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string MaterialDescription { get; set; }

        [DisplayName("Malzeme Miktarı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public decimal Quantity { get; set; }

        [DisplayName("Birim Ücreti")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
