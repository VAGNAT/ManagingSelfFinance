using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeExpenseController : ControllerBase
    {
        private readonly ICRUD<TypeExpense> _service;
        private readonly ILogger<TypeExpenseController> _logger;

        public TypeExpenseController(ICRUD<TypeExpense> service, ILogger<TypeExpenseController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all types of expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/TypeExpense
        ///     
        /// </remarks>
        /// <returns>All types of expense</returns>
        /// <response code="200">Returns all types of expense</response>
        /// <response code="404">No existing types of expense</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<TypeExpense>> Get()
        {
            _logger.LogInformation("Getting all types of expense");

            IEnumerable<TypeExpense> result = _service.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get the type of expense by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/TypeExpense/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A type of expense with this ID</returns>     
        /// <response code="200">Returns type of expense with this ID</response>
        /// <response code="404">There is no existing type of expense with this ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TypeExpense>> Get(int id)
        {
            _logger.LogInformation("Getting the type of expense by ID");

            TypeExpense res = await _service.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound(id);
            }

            return Ok(res);
        }

        /// <summary>
        /// Update a type of expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/TypeExpense
        ///     {
        ///     "id": "Id",
        ///     "name": "Name"
        ///     }
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A updated type of expense</returns>
        /// <response code="202">Returns updated type of expense with this ID</response>
        /// <response code="400">Id or name not specified</response>
        /// <response code="404">There is no existing type of expense with this ID</response>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post([FromBody] TypeExpense value)
        {
            _logger.LogInformation("Update the type of expense");

            if (value is null || value.Id == 0)
            {
                return BadRequest(value);
            }

            TypeExpense exist = await _service.GetByIdAsync(value.Id);
            if (exist == null)
            {
                return NotFound(value.Id);
            }
            
            TypeExpense result = _service.Update(exist, value);
            return AcceptedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// Creates a type of expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/TypeExpense
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created type of expense</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the value is null</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Put([FromBody] TypeExpense value)
        {
            _logger.LogInformation("Creating the type of expense");

            if (value is null)
            {
                return BadRequest(value);
            }

            TypeExpense res = await _service.AddAsync(value);
            return CreatedAtAction(nameof(Put), res);
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/TypeExpense/1
        ///     
        /// </remarks>
        /// <param name="id"></param>        
        /// <response code="200">The item has been removed</response>
        /// <response code="404">There is no existing type of expense with this ID</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete the type of expense by ID");

            if (!await _service.Delete(id))
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
