using Calculator.BusinessLogic.Dto;
using Calculator.BusinessLogic.MessageBroker.Base;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace Calculator.BusinessLogic.MessageBroker
{
    public class HistoryProducer : ProducerBase<CalculationResponseDto>
    {
        public HistoryProducer(ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger, 
            ILogger<ProducerBase<CalculationResponseDto>> producerBaseLogger) 
            : base(connectionFactory, logger, producerBaseLogger)
        {
        }

        protected override string AppId => "HistoryProducer";
    }
}
