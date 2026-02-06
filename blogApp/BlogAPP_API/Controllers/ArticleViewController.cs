using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Models;
using BlogAPP_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPP_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ArticleViewController : ControllerBase
    {

        private readonly IArticleService _articleService;

        public ArticleViewController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [Route("FindByProperties")]
        public async Task<IActionResult> FindByProperties(
           [FromBody] ArticlePropertiesFind propertiesFind)
        {
            try
            {
                List<ArticleReturnInAPI> list = await _articleService.FindArticleByProperties(propertiesFind);

                if (list == null || list.Count <=0)
                    return Ok(new { success = false, messegeEror = "Статьи не найдены" });

                return Ok(new { success = true, list });

            }
            catch (Exception ex)
            {
                return Ok(new { success = false, messegeEror = $"Ошибка: {ex.Message}" });
            }

        }
    }
}
