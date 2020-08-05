using System.Collections.Generic;
using Calculator.BusinessLogic.Dto.Calculation;

namespace Calculator.BusinessLogic.Dto.History
{
    public class HistoryResponseDto
    {
        public IReadOnlyCollection<CalculationResponseDto> Items { get; set; }
    }
}
