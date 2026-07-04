using Microsoft.AspNetCore.Mvc;
using eCommerce.Core.ServiceContracts;
using eCommerce.Core.DTO;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _userService;
        public AuthController(IUsersService userService)
        {
            _userService = userService;
        }

        //Endpoint for user registration use case
        [HttpPost("register")] //Post request to /api/auth/register
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            // Check for invalid registerRequest
            if (registerRequest == null)
            {
                return BadRequest("Invalid registration data");
            }

            // Call the service method to register the user
            AuthenticationResponse? authenticationResponse = await _userService.Register(registerRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false) 
            {
                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            // Check for invalid loginRequest
            if (loginRequest == null)
            {
                return BadRequest("Invalid login data");
            }

            AuthenticationResponse? authenticationResponse = await _userService.Login(loginRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false)
            {
                return Unauthorized(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }
    }
}
