using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculator.BusinessLogic.Contracts;
using System;

namespace CalculationHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> m_logger;
        private readonly IHistoryService m_service;

        public HistoryController(ILogger<HistoryController> logger, IHistoryService service)
        {
            m_logger = logger;
            m_service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await m_service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await m_service.GetById(id));
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLast()
        {
            return Ok(await m_service.GetLast());
        }

        [HttpGet("date={calculationDate}")]
        public async Task<IActionResult> GetByDate(DateTime calculationDate)
        {
            return Ok(await m_service.GetByDate(calculationDate));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await m_service.Remove(id);
            return Ok();
        }
    }
}
