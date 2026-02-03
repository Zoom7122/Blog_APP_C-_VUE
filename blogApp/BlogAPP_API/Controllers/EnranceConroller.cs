
using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Services;
using BlogAPP_Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogAPP_API.Controllers
{

    [Route("api/[controller]")]
    public class EntranceConroller : ControllerBase
    {
        private readonly ILoginService _logService;

        public EntranceConroller(ILoginService logService) 
        {
            _logService = logService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDate data)
        {
            var user = await _logService.Login(data);
            if (user == null)
                return Ok(new
                {
                    success = false,
                    messegeEror = "Неверный Email или пароль"
                });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Avatar", user.Avatar_url ?? ""),
                new Claim("Role", user.Role ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);



            return Ok(new { success = true , user });
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto data)
        {
            try
            {
                var result = await _logService.Register(data);

                if (result)
                    return Ok(new { success = true });
                return Ok(new { success = false });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    messegeEror = ex.Message
                });
            }
        }
    }
}
