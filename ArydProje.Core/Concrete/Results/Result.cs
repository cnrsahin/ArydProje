using ArydProje.Core.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Concrete.Results
{
    public class Result : IResult
    {
        public Result(Status status)
        {
            Status = status;
        }

        public Result(Status status, string message)
        {
            Status = status;
            Message = message;
        }

        public string Message { get; }
        public Status Status { get; }
    }
}
