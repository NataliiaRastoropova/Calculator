using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> m_logger;
        private readonly IService m_service;

        public CalculatorController(ILogger<CalculatorController> logger, IService service)
        {
            m_logger = logger;
            m_service = service;

        }




        // GET: api/<Calculator>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Calculator>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Calculator>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Calculator>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Calculator>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
