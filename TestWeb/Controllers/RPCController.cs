using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RPCController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            return new Person[] { new Person() };
        }

        [HttpGet]
        public ActionResult<Person> GetPerson(int id)
        {
            return new Person() {
                DateOfBirth = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Nommies",
                Surname = "Diepsloot"
            };
        }

        [HttpGet]
        public async Task<Receipt> GetReceipt(int personid, int receiptid)
        {
            return new Receipt()
            {
               
            };
        }

        [HttpPost]
        public ActionResult<Person> AddReceipt([FromBody] Person value)
        {
            return new Person()
            {
                DateOfBirth = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Nommies",
                Surname = "Diepsloot"
            };
        }

        [HttpPut]
        public async Task<Person> UpdateReceipt([FromQuery]int id, [FromBody] Person value)
        {
            return new Person()
            {
                DateOfBirth = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Nommies",
                Surname = "Diepsloot"
            };
        }

        [HttpDelete]
        public async Task<Person> DeletePerson(int id)
        {
            return new Person()
            {
                DateOfBirth = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Nommies",
                Surname = "Diepsloot"
            };
        }
    }
}