using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface ITagService
    {
        Task<bool> CreatrTagToArticle(Tag tag, string articleId);
    }
}
