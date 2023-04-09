using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ship_convenient.Services.ReportService;

namespace ship_convenient.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(Guid? accountId, int pageIndex = 0, int pageSize = 20)
        {
            var response = await _reportService.GetList(accountId, pageIndex, pageSize);
            return Ok(response);
        }
    }
}
