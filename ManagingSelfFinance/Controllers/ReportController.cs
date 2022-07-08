using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Model.ViewModel;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _service;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReport service, ILogger<ReportController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get daily report.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Report/2022.06.05
        ///     
        /// </remarks>
        /// <returns>Daily report</returns>
        /// <response code="200">Returns daily report</response>
        /// <response code="400">Date not specified</response>
        [HttpGet("{date}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<DailyReport> Get([FromRoute] string date)
        {
            _logger.LogInformation("Getting daily report");

            DateTime.TryParse(date, out DateTime dateResult);

            if (dateResult == DateTime.MinValue)
            {
                return BadRequest(date);
            }

            return Ok(_service.GetDailyReport(dateResult));
        }

        /// <summary>
        /// Get date period report.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Report?startDate=2022.06.01endDate=2022.06.30
        ///     
        /// </remarks>
        /// <returns>Date period report</returns>
        /// <response code="200">Returns date period report</response>
        /// <response code="400">Start date or end date not specified</response>
        /// <response code="400">If start date is greater than end date</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<DatePeriodReport> Get([FromQuery] string startDate, string endDate)
        {
            _logger.LogInformation("Getting date period report");

            DateTime.TryParse(startDate, out DateTime startDateResult);
            DateTime.TryParse(endDate, out DateTime endDateResult);

            if (startDateResult == DateTime.MinValue || endDateResult == DateTime.MinValue || startDateResult > endDateResult)
            {
                return BadRequest();
            }

            return Ok(_service.GetDatePeriodReport(startDateResult, endDateResult));
        }
    }
}
