using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApplication1.ServerApp.Сore.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.ServerApp.Application.Controllers
{
    [Route("user")]
    [Authorize(Policy = "RequireAdminPermission")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public UserController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("eventsubscribe")]
        public async Task<IResult> GetUsersForEvent(Guid eventId, Guid userId)
        {
            await _adminService.SubscribeForEvent(eventId, userId);
            return Results.Ok();
        }

        [HttpPost("eventunscribe")]
        public async Task<IResult> UnscribeEvent(Guid eventId, Guid userId)
        {
            await _adminService.UnscribeForEvent(eventId, userId);
            return Results.Ok();
        }
    }
}
