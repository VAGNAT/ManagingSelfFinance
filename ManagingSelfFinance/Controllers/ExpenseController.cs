using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ICRUD<Expense> _service;
        private readonly ICRUD<TypeExpense> _serviceType;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(ICRUD<Expense> expenseService, ICRUD<TypeExpense> _typeExpenseService, ILogger<ExpenseController> logger)
        {
            _service = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
            _serviceType = _typeExpenseService ?? throw new ArgumentNullException(nameof(_typeExpenseService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all expenses.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Expense
        ///     
        /// </remarks>
        /// <returns>All expenses</returns>
        /// <response code="200">Returns all expenses</response>
        /// <response code="404">No existing expense</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Expense>> Get()
        {
            _logger.LogInformation("Getting all expenses");

            IEnumerable<Expense> result = _service.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get the expense by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Expense/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A expense with this ID</returns>     
        /// <response code="200">Returns expense with this ID</response>
        /// <response code="404">There is no existing expense with this ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Expense> Get(int id)
        {
            _logger.LogInformation("Getting expense by ID");

            Expense res = _service.GetById(id);
            if (res == null)
            {
                return NotFound(id);
            }

            return Ok(res);
        }

        /// <summary>
        /// Update a expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Expense
        ///     {
        ///         "id": 1,
        ///         "date": "2022-06-05",
        ///         "typeExpense": {
        ///           "id": 1
        ///         },
        ///         "amount": 10
        ///     }
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A updated expense</returns>
        /// <response code="202">Returns updated expense with this ID</response>
        /// <response code="400">Id or date or amount or id of type not specified</response>
        /// <response code="404">There is no existing expense with this ID</response>
        /// <response code="404">There is no existing type of expense with this ID</response>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post([FromBody] Expense value)
        {
            _logger.LogInformation("Update the expense");

            if (value is null || value.Id == 0 || value.Date == DateTime.MinValue 
                || value.Amount == 0 || value.TypeExpense.Id == 0)
            {
                return BadRequest(value);
            }

            Expense exist = await _service.GetByIdAsync(value.Id);
            if (exist == null)
            {
                return NotFound(value.Id);
            }

            TypeExpense typeExist = await _serviceType.GetByIdAsync(value.TypeExpense.Id);
            if (typeExist == null)
            {
                return NotFound(value.TypeExpense.Id);
            }

            exist.TypeExpense = typeExist;
            Expense result = _service.Update(exist, value);
            return AcceptedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// Creates a expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Expense
        ///     {
        ///         "id": 1,
        ///         "date": "2022-06-05",
        ///         "typeExpense": {
        ///           "id": 1
        ///         },
        ///         "amount": 10
        ///     }
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created type of expense</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the value or date or amount or id of type is null</response>
        /// <response code="404">There is no existing type of expense with this ID</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put([FromBody] Expense value)
        {
            _logger.LogInformation("Creating the expense");

            if (value is null || value.Date == DateTime.MinValue || value.Amount == 0 || value.TypeExpense.Id == 0)
            {
                return BadRequest(value);
            }

            TypeExpense typeExist = await _serviceType.GetByIdAsync(value.TypeExpense.Id);
            if (typeExist == null)
            {
                return NotFound(value.TypeExpense.Id);
            }

            value.TypeExpense = typeExist;

            Expense res = await _service.AddAsync(value);
            return CreatedAtAction(nameof(Put), res);
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/Expense/1
        ///     
        /// </remarks>
        /// <param name="id"></param>        
        /// <response code="200">The item has been removed</response>
        /// <response code="404">There is no existing expense with this ID</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete the expense by ID");

            if (!await _service.Delete(id))
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
