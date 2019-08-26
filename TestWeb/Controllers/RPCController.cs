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
            return new Person();
        }

        [HttpGet]
        public async Task<Receipt> GetReceipt(int personid, int receiptid)
        {
            return new Receipt();
        }

        [HttpPost]
        public ActionResult<bool> AddReceipt([FromBody] Person value)
        {
            return true;
        }

        [HttpPut]
        public async Task<string> UpdateReceipt(int id, [FromBody] Person value)
        {
            return "success";
        }

        [HttpDelete]
        public async Task<string> DeletePerson(int id)
        {
            return "Deleted";
        }
    }
}