using AttendanceTrackerMicroservices.AuthAPI.Models;
using AttendanceTrackerMicroservices.AuthAPI.Models.DTO;
using AttendanceTrackerMicroservices.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerMicroservices.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthAPIController> _logger;
        protected ResponseDTO _response;

        public AuthAPIController(IAuthService authService, ILogger<AuthAPIController> logger)
        {
            _authService = authService;
            _logger = logger;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var errorMessage = await _authService.Register(registrationRequestDTO);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            _logger.LogInformation("AuthAPI - /login endpoint");
            var loginResponse = await _authService.Login(loginRequestDTO);

            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect!";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("validateUser")]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequestDTO loginRequestDTO)
        {
            ApplicationUser user = await _authService.ValidateUser(loginRequestDTO);

            if (user == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect!";
                return BadRequest(_response);
            }

            _response.Result = user;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO assignRoleRequestDTO)
        {
            bool assignRoleSuccessfully = await _authService.AssignRole(assignRoleRequestDTO.Email, assignRoleRequestDTO.Role);
            if (!assignRoleSuccessfully)
            {
                _response.IsSuccess = false;
                _response.Message = "Failed to assign role";
                return BadRequest(_response);
            }

            _response.Result = assignRoleSuccessfully;
            return Ok(_response);
        }

    }
}
