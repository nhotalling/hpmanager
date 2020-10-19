using System.Collections.Generic;
using DDB.HitPointManager.Domain;
using DDB.HitPointManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDB.HitPointManager.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
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

        /// <summary>
        /// Shows a character's current HP, maximum HP, and temporary HP.
        /// </summary>
        /// <param name="name">Character name - case-insensitive</param>
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

        /// <summary>
        /// Deals damage to the specified character, taking into account the character's damage
        /// immunity, vulnerability, and resistance
        /// </summary>
        /// <param name="name">Character name - case-insensitive</param>
        /// <param name="damage">Array of DamageRequest objects, each with its damage type
        /// and the value of damage dealt</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/v1/character/briv/damage
        ///     [
        ///         {
        ///             "type": "fire,
        ///             "value": 6
        ///         },
        ///         {
        ///             "type": "slashing",
        ///             "value": 9
        ///         }
        ///     ]
        /// 
        /// </remarks>
        [HttpPut("{name}/damage")]
        public ActionResult<CharacterHealth> DealDamage(string name, [FromBody] IEnumerable<DamageRequest> damage)
        {
            var result = _characterManager.DealDamage(name, damage);
            return Ok(result);
        }

        /// <summary>
        /// Heals hit points up to the character's maximum HP
        /// </summary>
        /// <param name="name">Character name - case-insensitive</param>
        /// <param name="value">Amount to heal - must be a positive integer</param>
        [HttpPut("{name}/heal")]
        public ActionResult<CharacterHealth> Heal(string name, int value)
        {
            var result = _characterManager.Heal(name, value);
            return Ok(result);
        }

        /// <summary>
        /// Adds temporary hit points, replacing the old value only if the new value is greater.
        /// May also be used with a negative number to remove temporary HP.
        /// </summary>
        /// <param name="name">Character name - case-insensitive</param>
        /// <param name="value">Add or replace current Temp HP value (does not stack; new value replaces old value if greater).
        /// Negative values allow you to remove Temp HP.</param>
        [HttpPut("{name}/temp")]
        public ActionResult<CharacterHealth> AddTempHp(string name, int value)
        {
            var result = _characterManager.AddTempHp(name, value);
            return Ok(result);
        }

    }
}
