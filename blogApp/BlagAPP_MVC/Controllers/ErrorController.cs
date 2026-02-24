using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlagAPP_MVC.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            ViewData["StatusCode"] = 500;
            return View("StatusCode");
        }

        [Route("Error/StatusCode/{statusCode}")]
        public IActionResult StatusCode(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }

            ViewData["StatusCode"] = statusCode;
            return View("StatusCode");
        }
    }
}