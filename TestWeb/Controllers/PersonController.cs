using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return new Person[] { new Person() {
                DateOfBirth = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Lol Me",
                Surname = "Schoeman",
                Receipts = new List<Receipt>()
            } };
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            return new Person();
        }

        [HttpGet("{personId}/{receiptId}")]
        public async Task<Receipt> Get(int personId, int receiptId)
        {
            return new Receipt();
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Person value)
        {
            return true;
        }

        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Person value)
        {
            return true;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return true;
        }
    }
}