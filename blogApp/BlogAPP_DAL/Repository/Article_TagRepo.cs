using blogApp_DAL;
using blogApp_DAL.Model;
using BlogAPP_DAL.Intarface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_DAL.Repository
{
    public class Article_TagRepo : IArticle_TagRepo
    {
        private readonly Blog_DBcontext _context;

        public Article_TagRepo(Blog_DBcontext context)
        {
            _context = context;
        }

        public async Task CreateRowTable(Article_Tag article_Tag)
        {
            var entry = _context.Entry(article_Tag);
            if (entry.State == EntityState.Detached)
                await _context.Article_Tags.AddAsync(article_Tag);

            await _context.SaveChangesAsync();
        }
    }
}
