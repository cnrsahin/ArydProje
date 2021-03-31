using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; set; }
        public int Status { get; set; }
    }
}
