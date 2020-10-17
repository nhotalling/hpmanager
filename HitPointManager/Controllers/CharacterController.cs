using System.Collections.Generic;
using DDB.HitPointManager.Domain;
using DDB.HitPointManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDB.HitPointManager.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{name}")]
        public ActionResult<Character> Get(string name)
        {
            // Typically we'd use a Dto here instead of returning the entity
            // And it would be good to wire up a global exception handler to 
            // handle catching certain exceptions to return appropriate status codes
            // (like 404 when and object isn't found)

            var result = _characterService.GetCharacter(name);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
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
