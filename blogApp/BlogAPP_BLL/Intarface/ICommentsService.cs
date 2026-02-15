using BlogAPP_BLL.Models;
using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface ICommentsService
    {
        Task<bool> CreateComments(CommentModelsCreate modelsCreate);

        Task<int> GetCountCommentsWroteByUser(string userId);

        Task<List<CommentsViewModel>> ArticleComments(Article article);

        Task<bool> DeleteCommentAsync(string commentId);

        Task<bool> IsCommentOwnedByUserAsync(string commentId, string userId);
    }
}
