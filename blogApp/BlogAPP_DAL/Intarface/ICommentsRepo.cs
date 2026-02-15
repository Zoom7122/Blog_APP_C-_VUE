using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_DAL.Intarface
{
    public interface ICommentsRepo
    {

        Task AddCommetsToArticle(Comment comment);


        Task<int> FindCountCommetsWroteByUser(string userId);

        Task<List<Comment>> FindCommentsArticle(string ArticleId);

        Task<bool> DeleteCommentByIdAsync(string commentId);

        Task<bool> IsCommentOwnedByUserAsync(string commentId, string userId);
    }
}
