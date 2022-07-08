using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ICRUD<Income> _service;
        private readonly ICRUD<TypeIncome> _serviceType;
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(ICRUD<Income> incomeService, ICRUD<TypeIncome> _typeIncomeService, ILogger<IncomeController> logger)
        {
            _service = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
            _serviceType = _typeIncomeService ?? throw new ArgumentNullException(nameof(_typeIncomeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all incomes.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Income
        ///     
        /// </remarks>
        /// <returns>All incomes</returns>
        /// <response code="200">Returns all incomes</response>
        /// <response code="404">No existing income</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Income>> Get()
        {
            _logger.LogInformation("Getting all incomes");

            IEnumerable<Income> result = _service.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get the income by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Income/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A income with this ID</returns>     
        /// <response code="200">Returns income with this ID</response>
        /// <response code="404">There is no existing income with this ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Income> Get(int id)
        {
            _logger.LogInformation("Getting income by ID");

            Income res = _service.GetById(id);
            if (res == null)
            {
                return NotFound(id);
            }

            return Ok(res);
        }

        /// <summary>
        /// Update a income.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Income
        ///     {
        ///         "id": 1,
        ///         "date": "2022-06-05",
        ///         "typeIncome": {
        ///           "id": 1
        ///         },
        ///         "amount": 10
        ///     }{
        ///     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A updated income</returns>
        /// <response code="202">Returns updated income with this ID</response>
        /// <response code="400">Id or date or amount or id of type not specified</response>
        /// <response code="404">There is no existing income with this ID</response>
        /// <response code="404">There is no existing type of income with this ID</response>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post([FromBody] Income value)
        {
            _logger.LogInformation("Update the expense");

            if (value is null || value.Id == 0 || value.Date == DateTime.MinValue
                || value.Amount == 0 || value.TypeIncome.Id == 0)
            {
                return BadRequest(value);
            }

            Income exist = await _service.GetByIdAsync(value.Id);
            if (exist == null)
            {
                return NotFound(value.Id);
            }

            TypeIncome typeExist = await _serviceType.GetByIdAsync(value.TypeIncome.Id);
            if (typeExist == null)
            {
                return NotFound(value.TypeIncome.Id);
            }

            exist.TypeIncome = typeExist;
            Income result = _service.Update(exist, value);

            return AcceptedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// Creates a income.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Income
        ///     {
        ///         "id": 1,
        ///         "date": "2022-06-05",
        ///         "typeIncome": {
        ///           "id": 1
        ///         },
        ///         "amount": 10
        ///     }     
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created type of income</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the value or date or amount or id of type is null</response>
        /// <response code="404">There is no existing type of income with this ID</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put([FromBody] Income value)
        {
            _logger.LogInformation("Creating the expense");

            if (value is null || value.Date == DateTime.MinValue || value.Amount == 0 || value.TypeIncome.Id == 0)
            {
                return BadRequest(value);
            }

            TypeIncome typeExist = await _serviceType.GetByIdAsync(value.TypeIncome.Id);
            if (typeExist == null)
            {
                return NotFound(value.TypeIncome.Id);
            }

            value.TypeIncome = typeExist;
            Income res = await _service.AddAsync(value);

            return CreatedAtAction(nameof(Put), res);
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/Income/1
        ///     
        /// </remarks>
        /// <param name="id"></param>        
        /// <response code="200">The item has been removed</response>
        /// <response code="404">There is no existing income with this ID</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete the income by ID");

            if (!await _service.Delete(id))
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
