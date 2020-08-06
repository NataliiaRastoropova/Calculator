using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Dto;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> m_logger;
        private readonly ICalculatorService m_service;

        public CalculatorController(ILogger<CalculatorController> logger, ICalculatorService service)
        {
            m_logger = logger;
            m_service = service;
        }

        [HttpPost("/calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequestDto request)
        {
            return Ok(await m_service.Calculate(request));
        }
    }
}
