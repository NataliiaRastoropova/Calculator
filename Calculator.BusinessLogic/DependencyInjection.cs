using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Services;
using Calculator.BusinessLogic.Models;

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
        }
    }
}
