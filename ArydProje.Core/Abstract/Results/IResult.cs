using ArydProje.Core.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Results
{
    public interface IResult
    {
        public string Message { get; }
        public Status Status { get; }
    }
}
