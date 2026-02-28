using BlogAPP_BLL.Intarface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPP_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public UsersController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("Me")]
        public async Task<IActionResult> GetMe()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized(new { success = false, message = "Пользователь не авторизован" });

            var user = await _loginService.FindUserByEmail(email);
            if (user == null)
                return NotFound(new { success = false, message = "Пользователь не найден" });

            return Ok(new
            {
                success = true,
                user = new
                {
                    user.Id,
                    user.FirstName,
                    user.Email,
                    user.Avatar_url,
                    user.Bio,
                    user.Role
                }
            });
        }
    }
}
