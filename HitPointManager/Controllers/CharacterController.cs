﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DDB.HitPointManager.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CharacterController : ControllerBase
    {
        // GET: api/<CharacterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CharacterController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
