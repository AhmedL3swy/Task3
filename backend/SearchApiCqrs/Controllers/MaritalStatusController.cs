using DataGrid.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGrid.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MaritalStatusController : ControllerBase
    {
        public IMaritalStatusService _maritalStatusService;

        public MaritalStatusController(IMaritalStatusService maritalStatusService)
        {
            _maritalStatusService = maritalStatusService;
        }
        [HttpGet]
        public IActionResult GetMaritalStatuses()
        {

            var maritalStatuses = _maritalStatusService.GetAllMaritalStatuses();
            return Ok(maritalStatuses);

        }
    }
}
