using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Calculator
{
    public interface ICalculator
    {
        OrderLine TaxCalculate(OrderLine entity);
    }
}
