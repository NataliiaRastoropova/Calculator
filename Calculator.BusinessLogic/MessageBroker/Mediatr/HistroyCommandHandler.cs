using Calculator.BusinessLogic.Dto;
using Calculator.BusinessLogic.Models;
using Calculator.BusinessLogic.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calculator.BusinessLogic.MessageBroker.Mediatr
{
    public class HistroyCommandHandler : IRequestHandler<CalculationResponseDto>
    {
        private readonly CalculationHistoryRepository m_historyRepository;

        public HistroyCommandHandler(ICalculatorDatabaseSettings databaseSettings)
        {
            m_historyRepository = new CalculationHistoryRepository(databaseSettings);
        }
        public async Task<Unit> Handle(CalculationResponseDto calculationResponse, CancellationToken cancellationToken)
        {
            await m_historyRepository.Create(new Models.CalculationHistoryModel
            {
                CalculationDate = calculationResponse.CalculationDate,
                Result = calculationResponse.ResultValue,
                Equation = calculationResponse.Equation
            });

            return Unit.Value;
        }
    }
}
