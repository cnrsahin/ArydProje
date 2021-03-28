using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Dtos
{
    public class HeaderWithLinesDto
    {
        public IEnumerable<LineToHeaderDto> OrderLines { get; set; }
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
