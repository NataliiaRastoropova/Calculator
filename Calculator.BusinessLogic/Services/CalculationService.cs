using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Dto.Calculation;
using Calculator.BusinessLogic.Exceptions;
using Calculator.BusinessLogic.Helpers;
using Calculator.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly CalculationHistoryRepository m_historyRepository;
        private readonly Dictionary<char, bool> operationsPriority = new Dictionary<char, bool>()
            {
                {'*', true},
                {'/', true},
                {'+', false},
                {'-', false},
            };

        public CalculatorService()
        {
            m_historyRepository = new CalculationHistoryRepository();
        }

        public async Task<CalculationResponseDto> Calculate(CalculationRequestDto model)
        {
            CalculationResponseDto calculationResponse = new CalculationResponseDto();

            if (model.Numbers.Length - 1 != model.Operations.Length)
            {
                throw new BadArgumentException("Operation length has to be less than numbers less for 1 character.");
            }

            bool isCalculating = true;
            List<decimal> tempNumbers = model.Numbers.ToList();
            List<char> tempOperations = model.Operations.ToList();

            while (isCalculating)
            {
                if (tempOperations.Any(o => operationsPriority[o]))
                {
                    for (var i = 0; i <= tempOperations.Count; i++)
                    {
                        if (operationsPriority[tempOperations[i]])
                        {
                            if (tempOperations[i]=='/' && tempNumbers[i+1]==0)
                            {
                                throw new BadArgumentException("Division by zero");
                            }

                            calculationResponse.ResultValue = CalculateIteration(ref tempNumbers, ref tempOperations, i);
                            break;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i <= tempOperations.Count; i++)
                    {
                        calculationResponse.ResultValue = CalculateIteration(ref tempNumbers, ref tempOperations, i);
                        break;
                    }
                }

                isCalculating = tempOperations.Count != 0;
            }

            //calculationResponse.Equation = ;
            calculationResponse.CalculationDate = DateTime.UtcNow;

            await m_historyRepository.Create(new Models.CalculationHistoryModel
            {
                CalculationDate = calculationResponse.CalculationDate,
                Result = calculationResponse.ResultValue,
                Equation = calculationResponse.Equation
            });

            return calculationResponse;
        }

        private decimal CalculateIteration(ref List<decimal> numbers, ref List<char> operations, int index)
        {
            var result = CalculatorHelper.GetResult(operations[index], numbers[index], numbers[index + 1]);
            operations.RemoveAt(index);
            numbers[index] = result;
            numbers.RemoveAt(index + 1);
            return result;
        }
    }
}
