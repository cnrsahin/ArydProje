using ArydProje.Core.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Concrete.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(Status status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public DataResult(Status status, T data)
        {
            Status = status;
            Data = data;
        }

        public DataResult(T data)
        {
            Data = data;
        }
        public DataResult(Status status)
        {
            Status = status;
        }
        public T Data { get; }
        public string Message { get; }
        public Status Status { get; }

    }
}
