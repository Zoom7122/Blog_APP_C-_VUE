using AutoMapper;
using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Models;
using blogApp_DAL.Intarface;
using blogApp_DAL.Model;
using blogApp_DAL.Repository;
using BlogAPP_DAL.Intarface;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepo _commentsRepo;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepo commentsRepo, IMapper mapper)
        {
            _commentsRepo = commentsRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateComments(CommentModelsCreate modelsCreate)
        {

            if(string.IsNullOrEmpty(modelsCreate.UserId))
                throw new Exception("UserId is empty");

            if (string.IsNullOrEmpty(modelsCreate.Content))
                throw new Exception("Content is empty");

            if (string.IsNullOrEmpty(modelsCreate.ArticleId))
                throw new Exception("ArticleId is empty");

            var comment = _mapper.Map<Comment>(modelsCreate);

            comment.CreatedAt = DateTime.Now;

            await _commentsRepo.AddCommetsToArticle(comment);

            return true;
        }
    }
}
