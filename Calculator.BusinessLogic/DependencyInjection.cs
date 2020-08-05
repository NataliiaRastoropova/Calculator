using Microsoft.Extensions.DependencyInjection;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Services;

namespace Calculator.BusinessLogic
{
    public static class DependencyInjection
    {
        public static void AddDateBase(this IServiceCollection services)
        {
            services.AddScoped<IService, CalculationHistoryService>();
        }
    }
}
