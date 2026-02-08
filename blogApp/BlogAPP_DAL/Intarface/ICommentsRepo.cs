using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_DAL.Intarface
{
    public interface ICommentsRepo
    {

        Task AddCommetsToArticle(Comment comment);

    }
}
