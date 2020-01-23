using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IGetInvoicesCommand getInvoices;

        public InvoicesController(IGetInvoicesCommand getInvoices)
        {
            this.getInvoices = getInvoices;
        }

        // GET: api/Invoices
        [HttpGet]
        public IActionResult Get([FromQuery] InvoiceQuery query)
        {
            try
            {
                return Ok(getInvoices.Execute(query));
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Errors = new List<string> { e.Message }
                });
            }
        }

        // GET: api/Invoices/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invoices
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
