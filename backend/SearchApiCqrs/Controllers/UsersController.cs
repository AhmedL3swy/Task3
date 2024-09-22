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
using System.Linq;

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
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] NewUserDto userDto)
        {
            var user = _mapper.Map<NewUserDto, User>(userDto);
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);

        }
        // Edit User
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditUser([FromBody] UserDto userDto, [FromRoute] int id)
        {
            userDto.Id = id;
            // Retrieve the existing user
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Map non-null fields from userDto to the existing user
            _mapper.Map(userDto, existingUser);

            // Validate the updated user object
            var validationResult = await _userValidator.ValidateAsync(existingUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            // Update the user
            await _userRepository.UpdateAsync(existingUser);

            return CreatedAtAction(nameof(GetUser), new { id = existingUser.Id }, existingUser);
        }

        #region Validators Api
        [HttpGet]
        public async Task<IActionResult> IsUnique([FromQuery] string propertyName, [FromQuery] string value, [FromQuery] int id)
        {
            var user = new User();
            user.Id = id;
            switch (propertyName)
            {
                case "Email":
                    user.Email = value;
                    break;
                case "Mobile":
                    user.Mobile = value;
                    break;
                case "MunicipalNo":
                    user.MunicipalNo = value;
                    break;
                case "NationalId":
                    user.NationalId = value;
                    break;
                default:
                    return BadRequest("Invalid property name.");
            }

            var isUnique = await _userRepository.IsUniqueAsync(user, propertyName, "Id");
            return Ok(isUnique);
        }
        #endregion



    }
}
