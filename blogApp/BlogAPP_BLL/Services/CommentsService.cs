using AutoMapper;
using BlogAPP_BLL.Exceptions;
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
        private readonly IUserRepo _userRepo;

        public CommentsService(ICommentsRepo commentsRepo, IMapper mapper, IUserRepo userRepo)
        {
            _commentsRepo = commentsRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<List<CommentsViewModel>> ArticleComments(Article article)
        {
            List<CommentsViewModel> commentsToPush = new List<CommentsViewModel>();


            var user = await _userRepo.FindUserByEmail(article.Author_Email);

            if (user == null)
                throw new UserNotFoundException();

            var comments = await _commentsRepo.FindCommentsArticle(article.Id);

            for (int i = 0; i < comments.Count; i++)
            {
                commentsToPush.Add(_mapper.Map<CommentsViewModel>(comments[i]));
                commentsToPush[i].UserName = user.FirstName;
            }

            return commentsToPush;
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

        public async Task<int> GetCountCommentsWroteByUser(string userId)
        {
            return await _commentsRepo.FindCountCommetsWroteByUser(userId);
        }
    }
}
