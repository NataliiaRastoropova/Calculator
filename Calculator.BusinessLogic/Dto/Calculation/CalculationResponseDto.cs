using System;

namespace Calculator.BusinessLogic.Dto.Calculation
{
    public class CalculationResponseDto
    {
        public string Equation { get; set; }
        public decimal ResultValue { get; set; }
        public DateTime CalculationDate { get;set; }
    }
}
