using aspnetcore9_jwt.Dtos;
using aspnetcore9_jwt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore9_jwt.Web.Rest;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        var result = await _authService.Authenticate(loginRequestDto);
        if (result is null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }

}
