using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeIncomeController : ControllerBase
    {
        private readonly ICRUD<TypeIncome> _service;
        private readonly ILogger<TypeIncomeController> _logger;

        public TypeIncomeController(ICRUD<TypeIncome> service, ILogger<TypeIncomeController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all types of income.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/TypeIncome
        ///     
        /// </remarks>
        /// <returns>All types of income</returns>
        /// <response code="200">Returns all types of income</response>
        /// <response code="404">No existing types of income</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<TypeIncome>> Get()
        {
            _logger.LogInformation("Getting all types of income");

            IEnumerable<TypeIncome> result = _service.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get the type of income by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/TypeIncome/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A type of income with this ID</returns>     
        /// <response code="200">Returns type of income with this ID</response>
        /// <response code="404">There is no existing type of income with this ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TypeIncome>> Get(int id)
        {
            _logger.LogInformation("Getting the type of income by ID");

            TypeIncome res = await _service.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound(id);
            }

            return Ok(res);
        }

        /// <summary>
        /// Update a type of income.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/TypeIncome
        ///     {
        ///     "id": "Id",
        ///     "name": "Name"
        ///     }
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A updated type of income</returns>
        /// <response code="202">Returns updated type of income with this ID</response>
        /// <response code="400">Id or name not specified</response>
        /// <response code="404">There is no existing type of income with this ID</response>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post([FromBody] TypeIncome value)
        {
            _logger.LogInformation("Update the type of income");

            if (value is null || value.Id == 0)
            {
                return BadRequest(value);
            }

            TypeIncome exist = await _service.GetByIdAsync(value.Id);
            if (exist == null)
            {
                return NotFound(value.Id);
            }
                        
            TypeIncome result = _service.Update(exist, value);
            return AcceptedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// Creates a type of income.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/TypeIncome
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created type of income</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the value is null</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Put([FromBody] TypeIncome value)
        {
            _logger.LogInformation("Creating the type of income");

            if (value is null)
            {
                return BadRequest(value);
            }

            TypeIncome res = await _service.AddAsync(value);
            return CreatedAtAction(nameof(Put), res);
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/TypeIncome/1
        ///     
        /// </remarks>
        /// <param name="id"></param>        
        /// <response code="200">The item has been removed</response>
        /// <response code="404">There is no existing type of income with this ID</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete the type of income by ID");

            if (!await _service.Delete(id))
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
