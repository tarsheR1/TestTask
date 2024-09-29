using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.ServerApp.Application.Services;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.ServerApp.Сore.Contracts;

[Route("api/authorization")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(IAuthService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("register")]
    public async Task<IResult> Register(
        [FromBody] RegisterUserRequest request)
    {
        await _userService.Register(request);
        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginUserRequest request)
    {
        var token = await _userService.Login(request);

        var httpContext = _httpContextAccessor.HttpContext;
        httpContext.Response.Cookies.Append("Titul", token);
     
        return Results.Ok(token);
    }
}


