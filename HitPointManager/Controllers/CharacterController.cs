﻿using System.Collections.Generic;
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
        private readonly ICharacterManager _characterManager;

        public CharacterController(ICharacterManager characterManager)
        {
            _characterManager = characterManager;
        }

        /// <summary>
        /// Gets json model for the given character to review its stats and defenses.
        /// </summary>
        /// <param name="name">Character name - case-insensitive</param>
        [HttpGet("{name}")]
        public ActionResult<Character> Get(string name)
        {
            // Typically we'd use a Dto here instead of returning the entity
            // And it would be good to wire up a global exception handler to 
            // handle catching certain exceptions to return appropriate status codes
            // (like 404 when an object isn't found)

            var result = _characterManager.GetCharacter(name);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{name}/status")]
        public ActionResult<CharacterHealth> GetStatus(string name)
        {
            var result = _characterManager.GetStatus(name);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{name}/damage")]
        public ActionResult<CharacterHealth> DealDamage(string name, [FromBody] IEnumerable<DamageRequest> damage)
        {
            var result = _characterManager.DealDamage(name, damage);
            return Ok(result);
        }

        [HttpPut("{name}/heal")]
        public ActionResult<CharacterHealth> Heal(string name, int value)
        {
            var result = _characterManager.Heal(name, value);
            return Ok(result);
        }

        [HttpPut("{name}/temp")]
        public ActionResult<CharacterHealth> AddTempHp(string name, int value)
        {
            var result = _characterManager.AddTempHp(name, value);
            return Ok(result);
        }

    }
}
