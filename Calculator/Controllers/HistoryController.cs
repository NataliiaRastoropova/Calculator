using System.Threading.Tasks;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Dto.History;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<CalculatorController> m_logger;
        private readonly IHistoryService m_service;

        public HistoryController(ILogger<CalculatorController> logger, IHistoryService service)
        {
            m_logger = logger;
            m_service = service;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLast()
        {
            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(HistoryRequestDto historyRequestDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
