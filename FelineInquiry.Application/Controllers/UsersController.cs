using AutoMapper;
using Azure;
using FelineInquiry.Core.DTOs.Users;
using FelineInquiry.Domain.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User = FelineInquiry.Core.Models.Entities.Users.User;

namespace FelineInquiry.Application.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager<User> _usersManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        public UsersController(IUserManager<User> usersManager, ILogger<UsersController> logger, IMapper mapper)
        {
            _usersManager = usersManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Register new user using registerDto model
        /// </summary>
        /// <param name="input"> Represnts Accepted JSON object to register new User </param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegisterDto input)
        {
            try
            {
                _logger.LogInformation($"Register User Controller: Trying to register user {input.Email}");

                (IdentityResult result, User user) = await _usersManager.CreateUserAsync(input);

                if (result != null)
                {
                    return BadRequest(result.Errors.First().Description);
                }

                UserResultDto userResult = _mapper.Map<UserResultDto>(user);

                _logger.LogInformation($"Register User Controller: register user {input.Email} Successfully!");

                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while Registering User {input.Email} ", ex);

                return StatusCode(500, "A problem happend when trying to handle your request");
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto input)
        {
            (IdentityResult? result, User? user, UserToken? tokens) = await _usersManager.LoginUserAsync(input);

            if (result == null && user == null)
            {
                return NotFound();
            }

            if (result != null)
            {
                return BadRequest(result.Errors.First().Description);
            }

            return Ok(tokens);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            string? userId = User.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            User? user = await _usersManager.FindUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            UserResultDto userResult = _mapper.Map<UserResultDto>(user);

            return Ok(userResult);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            IdentityResult result = await _usersManager.DeleteUserAsync(userId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.First().Description);
            }

            return NoContent();
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDto input)
        {
            string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            (IdentityResult? result, User? user) = await _usersManager.UpdateUserAsync(input, userId);

            if (result != null)
            {
                return BadRequest(result.Errors.First().Description);
            }

            UserResultDto userResult = _mapper.Map<UserResultDto>(user);

            return Ok(userResult);
        }

        [HttpPatch]
        [Authorize]
        // JSON Patch document
        public async Task<IActionResult> Update(JsonPatchDocument<UpdateUserDto> input)
        {
            // Move this to user manager
            input.ApplyTo(new UpdateUserDto() /*add real object from db store here*/, ModelState);

            // Move this check to user manager just return bool
            // this part checks the validity of the JSON patch document (the operations, and properties)
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // this part checks the validity of the upcoming object to match the
            // validation rules on fields (maxLength, Required ... etc)
            if (!TryValidateModel(input))
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
