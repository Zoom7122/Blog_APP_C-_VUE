using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface IArticle_TagService
    {
        Task<bool> CreateArticleTagConnection(string tagId, string articleId);
    }
}
