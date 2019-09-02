using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBuilder.JS;
using ContractExtractor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ICodeBuilder.Attributes;

namespace TestWeb.Controllers
{
    [ExcludeFromAPIContract]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<String> Get()
        {
            var result = new ClassTraveler(true).TravelClasses(new JavaScript() { });
            return JsonConvert.SerializeObject(new { result.Classes, result.Models }, Formatting.Indented);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
