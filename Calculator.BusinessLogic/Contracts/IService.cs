using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BusinessLogic.Models;

namespace Calculator.BusinessLogic.Contracts
{
    public interface IService
    {
        Task Create(CalculationHistory model);
        Task Update(CalculationHistory model);
        Task Remove(string id);
        Task<CalculationHistory> GetById(string id);
        Task<IEnumerable<CalculationHistory>> GetAll();
        Task<IEnumerable<CalculationHistory>> GetByDate(DateTime date);
    }
}
