using System;

namespace Calculator.BusinessLogic.Dto
{
    public class CalculationResponseDto
    {
        public string Id { get; set; }
        public string Equation { get; set; }
        public decimal ResultValue { get; set; }
        public DateTime CalculationDate { get;set; }
    }
}
