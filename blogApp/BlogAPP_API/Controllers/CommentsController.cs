using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPP_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly ILoginService _loginService;

        public CommentsController(ICommentsService commentsService, ILoginService loginService)
        {
            _commentsService = commentsService;
            _loginService = loginService;
        }

        [HttpPost]
        [Route("CreateComments")]
        public async Task<IActionResult> CreateComments([FromBody] CommentModelsCreate comment)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrWhiteSpace(email))
                    return Unauthorized(new { success = false, message = "Пользователь не авторизован" });

                var user = await _loginService.FindUserByEmail(email);
                if (user == null)
                    return NotFound(new { success = false, message = "Пользователь не найден" });

                comment.UserId = user.Id;

                var result = await _commentsService.CreateComments(comment);
                if (!result)
                    return BadRequest(new { success = false, message = "Не удалось создать комментарий" });

                return Created(string.Empty, new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            try
            {
                var result = await _commentsService.DeleteCommentAsync(commentId);

                if (!result)
                    return NotFound(new { success = false, message = "Комментарий не найден" });

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetCountComments")]
        public async Task<IActionResult> GetCountComments()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrWhiteSpace(userEmail))
                    return Unauthorized(new { success = false, message = "Пользователь не авторизован" });

                var user = await _loginService.FindUserByEmail(userEmail);
                if (user == null)
                    return NotFound(new { success = false, message = "Пользователь не найден" });

                var countComments = await _commentsService.GetCountCommentsWroteByUser(user.Id);
                return Ok(new { countComments });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}