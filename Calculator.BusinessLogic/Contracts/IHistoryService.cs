using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BusinessLogic.Dto;

namespace Calculator.BusinessLogic.Contracts
{
    public interface IHistoryService
    {
        Task<IEnumerable<CalculationResponseDto>> GetAll();
        Task<CalculationResponseDto> GetById(string id);
        Task<CalculationResponseDto> GetLast();
        Task<IEnumerable<CalculationResponseDto>> GetByDate(DateTime date);
        Task Remove(string id);
    }
}
