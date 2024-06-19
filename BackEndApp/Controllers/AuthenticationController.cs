using Commons.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AuthService;

namespace BackEndApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        readonly IAuthService authService;

        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<AuthResponse>>> Login(AuthRequest authRequest)
        {
            ActionResult response;

            var userResponse = await authService.Authenticate(authRequest);

            if (userResponse.status == 200)
            { response = Ok(userResponse); }
            else
            { response = Unauthorized(userResponse); }

            return response;
        }

    }
}
