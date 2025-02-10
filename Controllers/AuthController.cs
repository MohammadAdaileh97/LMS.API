using LMS.Core.Data;
using LMS.Core.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login login)
        {
            var result = _authService.Login(login);
            if (result == null) return Unauthorized("Invalid UserName Or Password");
            return Ok(result);
        }
    }
}
