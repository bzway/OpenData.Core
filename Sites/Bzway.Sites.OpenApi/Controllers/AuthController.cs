using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bzway.Sites.OpenApi.Models;

namespace Bzway.Sites.OpenApi.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
      
        public IEnumerable<string> Token(string appid, GrantViewModel model)
        {
            return new string[] { model.appid, model.secret };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
