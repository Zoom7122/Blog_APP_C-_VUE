using AutoMapper;
using BlogAPP_BLL.Exceptions;
using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Models;
using BlogAPP_Core.Models;
using blogApp_DAL.Intarface;
using blogApp_DAL.Model;
using BlogAPP_DAL.Intarface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepo _articleRepo;

        public ArticleService(IArticleRepo articleRepo, IMapper mapper)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
        }

        public async Task<int> CountArticleWroteByUserAsync(string email)
        {
            return await _articleRepo.GetCountArticleInDbPostByUserAsync(email);
        }

        public async Task<List<ArticleReturnInAPI>> FindArticleByTitile(string title)
        {
            var listArticle = await _articleRepo.GetArticleByTitileAsync(title);

            List<ArticleReturnInAPI> listArticleToApi = new List<ArticleReturnInAPI>();

            for (int i = 0; i < listArticle.Count; i++)
            {
                var article = _mapper.Map<ArticleReturnInAPI>(listArticle[i]);
                listArticleToApi.Add(article);
            }

            return listArticleToApi;
        }

        public async Task<bool> CreateArticle(CreateArticleModel model, UserCookie userCooki)
        {
            if (model.Title == null)
                throw new ArticleException("Заполните название");

            if (model.text == null)
                throw new ArticleException("Текст не null");

            if (model.ReadTime <= 0)
                throw new ArticleException("Заполните время чтения");

            if (model.description == null)
                throw new ArticleException("Заполните описание");

            if (userCooki == null && userCooki.Email == null)
                throw new ArticleException("Необходим email из кук");

            var article = _mapper.Map<Article>(model);

            article.Author_Name = userCooki.Name;

            article.Author_Email = userCooki.Email;

            article.PublishedAt = DateTime.Now;

            _articleRepo.CreateArticleinDbAsync(article);

            return true;
        }

        public async Task<List<ArticleReturnInAPI>> FindArticleByProperties(
            ArticlePropertiesFind properties)
        {
            if (properties == null)
                return null;

            if(properties.Title == null && properties.Tag != null)
            {
                var listArticle = await _articleRepo.GetArticleByTagAsync(properties.Tag);

                if (listArticle == null) return null;

                List<ArticleReturnInAPI> listArticleToApi = new List<ArticleReturnInAPI>();

                for (int i = 0; i < listArticle.Count; i++)
                {
                    var article = _mapper.Map<ArticleReturnInAPI>(listArticle[i]);
                    listArticleToApi.Add(article);
                }

                return listArticleToApi;
            }
            else if (properties.Title != null && properties.Tag == null)
            {
                var listArticle = await _articleRepo.GetArticleByTitileAsync(properties.Title);

                if (listArticle == null) return null;

                List<ArticleReturnInAPI> listArticleToApi = new List<ArticleReturnInAPI>();

                for (int i = 0; i < listArticle.Count; i++)
                {
                    var article = _mapper.Map<ArticleReturnInAPI>(listArticle[i]);
                    listArticleToApi.Add(article);
                }

                return listArticleToApi;
            }
            else if (properties.Title != null && properties.Tag != null)
            {
                var listArticle = await _articleRepo.GetArticleByTitileANDTagAsync(properties);

                if (listArticle == null) return null;

                List<ArticleReturnInAPI> listArticleToApi = new List<ArticleReturnInAPI>();

                for (int i = 0; i < listArticle.Count; i++)
                {
                    var article = _mapper.Map<ArticleReturnInAPI>(listArticle[i]);
                    listArticleToApi.Add(article);
                }

                return listArticleToApi;
            }

            return null;
        }
    }
}
