using AuthApplication.Models;
using AuthApplication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginService _loginService;

    public AuthController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthApplication.Models.LoginRequest request)
    {
        var result = _loginService.Authenticate(request);
        return result == null ? Unauthorized() : Ok(result);
    }
}
