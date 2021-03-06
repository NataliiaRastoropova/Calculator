﻿using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Dto;
using Calculator.BusinessLogic.Exceptions;
using Calculator.BusinessLogic.Helpers;
using Calculator.BusinessLogic.MessageBroker;
using Calculator.BusinessLogic.Models;
using Calculator.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly HistoryProducer m_producer;
        private readonly CalculationHistoryRepository m_historyRepository;
        private readonly Dictionary<char, bool> operationsPriority = new Dictionary<char, bool>()
            {
                {'*', true},
                {'/', true},
                {'+', false},
                {'-', false},
            };

        public CalculatorService(ICalculatorDatabaseSettings databaseSettings, HistoryProducer produce)
        {
            m_historyRepository = new CalculationHistoryRepository(databaseSettings);
            m_producer = produce;
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

            for (var i = 0; i <= tempOperations.Count-1; i++)
            {
                calculationResponse.Equation += tempNumbers[i] + " " + tempOperations[i] + " ";
            }
            calculationResponse.Equation += tempNumbers.Last();

            while (isCalculating)
            {
                if (tempOperations.Any(o => operationsPriority[o]))
                {
                    for (var i = 0; i <= tempOperations.Count-1; i++)
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
                    for (var i = 0; i <= tempOperations.Count-1; i++)
                    {
                        calculationResponse.ResultValue = CalculateIteration(ref tempNumbers, ref tempOperations, i);
                        break;
                    }
                }
                isCalculating = tempOperations.Count != 0;
            }

            calculationResponse.CalculationDate = DateTime.UtcNow;

            //await m_historyRepository.Create(new Models.CalculationHistoryModel
            //{
            //    CalculationDate = calculationResponse.CalculationDate,
            //    Result = calculationResponse.ResultValue,
            //    Equation = calculationResponse.Equation
            //});

            m_producer.Publish(calculationResponse);

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
