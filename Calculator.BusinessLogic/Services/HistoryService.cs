using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Dto;
using Calculator.BusinessLogic.Exceptions;
using Calculator.BusinessLogic.Models;
using Calculator.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly CalculationHistoryRepository m_historyRepository;
        public HistoryService(ICalculatorDatabaseSettings databaseSettings)
        {
            m_historyRepository = new CalculationHistoryRepository(databaseSettings);
        }

        public async Task<IEnumerable<CalculationResponseDto>> GetAll()
        {
            var model = (await m_historyRepository.GetAll()).Select(h => new CalculationResponseDto
            {
                Id = h.Id,
                Equation = h.Equation,
                ResultValue = h.Result,
                CalculationDate = h.CalculationDate
            });
            return model;
        }

        public async Task<CalculationResponseDto> GetById(string id)
        {
            var h = await m_historyRepository.GetById(id);
            if (h is null)
            {
                throw new DataNotFoundException($"History item for id {id} not found");
            }
            return new CalculationResponseDto
            {
                Id = h.Id,
                Equation = h.Equation,
                ResultValue = h.Result,
                CalculationDate = h.CalculationDate
            };
        }

        public async Task<CalculationResponseDto> GetLast()
        {
            var h = await m_historyRepository.GetLast();
            if (h is null)
            {
                throw new DataNotFoundException($"There is no items in database");
            }
            return new CalculationResponseDto
            {
                Id = h.Id,
                Equation = h.Equation,
                ResultValue = h.Result,
                CalculationDate = h.CalculationDate
            };
        }

        public async Task<IEnumerable<CalculationResponseDto>> GetByDate(DateTime date)
        {
            var model = (await m_historyRepository.GetByDate(date)).Select(h => new CalculationResponseDto
            {
                Id = h.Id,
                Equation = h.Equation,
                ResultValue = h.Result,
                CalculationDate = h.CalculationDate
            });
            return model;
        }

        public async Task Remove(string id)
        {
            await m_historyRepository.Remove(id);
        }
    }
}
