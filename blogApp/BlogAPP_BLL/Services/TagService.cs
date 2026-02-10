using BlogAPP_BLL.Intarface;
using blogApp_DAL;
using blogApp_DAL.Model;
using BlogAPP_DAL.Intarface;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlogAPP_BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepo _tagRepo;

        public TagService(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public Task<bool> CreatrTagToArticle(Tag tag, string articleId)
        {
            throw new NotImplementedException();
        }
    }
}
