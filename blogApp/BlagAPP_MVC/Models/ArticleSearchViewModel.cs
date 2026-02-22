using BlogAPP_BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace BlagAPP_MVC.Models
{
    public class ArticleSearchViewModel
    {
        [Display(Name = "Название")]
        public string? Title { get; set; }

        [Display(Name = "Тег")]
        public string? Tag { get; set; }

        public List<ArticleReturnInAPI> Articles { get; set; } = new();

        public bool SearchCompleted { get; set; }
    }
}
