using AutoMapper;
using DataGrid.Application.Contracts;
using DataGrid.Application.Features.Users.Query;
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
        public IMapper _mapper;

        public UsersController(ISearchRepository<User> searchRepository, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Search([FromBody] UserQueryDto query)
        {
            query.Include = "MaritalStatus";
            var result = await _searchRepository.SearchAsync(query);
            var mappedResult = _mapper.Map<SearchResult<User>, SearchResult<UserResultDto>>(result);
            return Ok(mappedResult);
        }

    }
}
