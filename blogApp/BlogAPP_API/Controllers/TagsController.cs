using BlogAPP_BLL.Intarface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPP_API.Controllers
{
    public class SyncTagsRequest
    {
        public List<string> Tags { get; set; } = new();
    }

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPut("Article/{articleId}")]
        public async Task<IActionResult> SyncArticleTags(string articleId, [FromBody] SyncTagsRequest request)
        {
            if (string.IsNullOrWhiteSpace(articleId))
                return BadRequest(new { success = false, message = "Не указан ID статьи" });

            await _tagService.SyncArticleTagsAsync(articleId, request?.Tags ?? new List<string>());
            return Ok(new { success = true });
        }
    }
}
