using DataGrid.Application.Contracts;
using DataGrid.Application.Shared.Models;
using DataGrid.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DataGrid.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public ISearchRepository<User> _searchRepository;

        public UsersController(ISearchRepository<User> searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchQuery query)
        {
            var result = await _searchRepository.SearchAsync(query);
            return Ok(result);
        }

    }
}
