using Microsoft.AspNetCore.Mvc;
using xpbro.DomainModels;
using xpbro_backend.Interfaces;

namespace xpbro_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet(Name = "GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(Guid userId)
        {
            UserProfile userProfile = await userService.GetUserProfileAsync(userId);
            return Ok(userProfile);
        }
    }
}
