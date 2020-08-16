using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Services;
using Calculator.BusinessLogic.Models;
using System;
using RabbitMQ.Client;
using Calculator.BusinessLogic.MessageBroker;
using System.Reflection;
using MediatR;

namespace Calculator.BusinessLogic
{
    public static class DependencyInjection
    {
        public static void AddDateBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CalculatorDatabaseSettings>(configuration.GetSection(nameof(CalculatorDatabaseSettings)));

            services.AddSingleton<ICalculatorDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<CalculatorDatabaseSettings>>().Value);

            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            services.AddScoped<HistoryProducer>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton(serviceProvider =>
            {
                var uri = new Uri("amqp://ftryswjv:FMocPFypneDDZby7BIiFjf_Zqcg2BiEU@buffalo.rmq.cloudamqp.com/ftryswjv");
                return new ConnectionFactory
                {
                    Uri = uri,
                    DispatchConsumersAsync = true
                };
            });
        }
    }
}
