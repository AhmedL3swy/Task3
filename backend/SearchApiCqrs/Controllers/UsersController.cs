using AutoMapper;
using DataGrid.Application.Contracts;
using DataGrid.Application.Features.Users.Command.AddUser;
using DataGrid.Application.Features.Users.Command.EditUser;
using DataGrid.Application.Features.Users.Query;
using DataGrid.Application.Shared.Models;
using DataGrid.Domain;
using DataGrid.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DataGrid.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public ISearchRepository<User> _searchRepository;
        public IMapper _mapper;
        public UserRepository _userRepository;
        public IValidator<User> _userValidator;

        public UsersController(ISearchRepository<User> searchRepository, IMapper mapper, UserRepository userRepository, IValidator<User> userValidator)
        {
            _searchRepository = searchRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userValidator = userValidator;
        }


        [HttpPost]
        public async Task<IActionResult> Search([FromBody] UserQueryDto query)
        {
            query.Include = "MaritalStatus";
            var result = await _searchRepository.SearchAsync(query);
            var mappedResult = _mapper.Map<SearchResult<User>, SearchResult<UserResultDto>>(result);
            return Ok(mappedResult);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] NewUserDto userDto)
        {
            var user = _mapper.Map<NewUserDto, User>(userDto);
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _userRepository.AddUser(user);
            return Created();
        }
        // Edit User
        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _userRepository.EditUser(user);
            return Ok();
        }

    }
}
