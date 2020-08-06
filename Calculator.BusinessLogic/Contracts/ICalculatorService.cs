using Calculator.BusinessLogic.Dto;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.Contracts
{
    public interface ICalculatorService
    {
        Task<CalculationResponseDto> Calculate(CalculationRequestDto model);
    }
}
