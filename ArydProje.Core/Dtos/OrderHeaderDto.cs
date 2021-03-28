using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Dtos
{
    public class OrderHeaderDto
    {
        public int Id { get; set; }
        
        [DisplayName("Tarih")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Fiş Numarası")]
        public int VoucherNo { get; set; }

        [DisplayName("Özel Kod")]
        public Guid SpecialCode { get; set; }

        [DisplayName("Proje Kodu")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(25, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string ProjectCode { get; set; }

        [DisplayName("Toplam Tutar")]
        public decimal TotalAmount { get; set; }
    }
}
