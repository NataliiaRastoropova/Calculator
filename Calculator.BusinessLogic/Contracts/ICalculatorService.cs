using Calculator.BusinessLogic.Dto.Calculation;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.Contracts
{
    public interface ICalculatorService
    {
        Task<CalculationResponseDto> Calculate(CalculationRequestDto model);
    }
}
