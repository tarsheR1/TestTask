using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.ServerApp.Сore.Interfaces;

namespace WebApplication1.ServerApp.Application.Controllers
{
    [Route("api/admin")]
    [Authorize(Policy = "RequireAdminPermission")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("userslist")]
        public async Task<IResult> GetUsersForEvent(Guid eventId)
        {
            var users = await _adminService.GetUsersForEvent(eventId);

            return Results.Ok(users);
        }

    }
}
