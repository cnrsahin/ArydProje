using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArydProje.Api.Models
{
    public class ApiCreateOrderModel
    {
        public NewHeaderModel NewHeaderModel { get; set; }
        public NewLineModel NewLineModel { get; set; }
    }
}
