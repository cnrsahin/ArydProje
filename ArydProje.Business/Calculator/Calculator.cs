using ArydProje.Core.Abstract.Calculator;
using ArydProje.Core.Concrete.Entities;
using ArydProje.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Business.Calculator
{
    public class Calculator : ICalculator
    {
        public OrderLine TaxCalculate(OrderLine entity)
        {
            var total = entity.Quantity * entity.UnitPrice;
            entity.TaxAmount = total / 100 * entity.TaxRate;
            entity.TotalAmount = total + entity.TaxAmount;

            return entity;
        }
    }
}
