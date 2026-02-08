using BlogAPP_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface ICommentsService
    {
        Task<bool> CreateComments(CommentModelsCreate modelsCreate);

        Task<int> GetCountCommentsWroteByUser(string userId);

    }
}
