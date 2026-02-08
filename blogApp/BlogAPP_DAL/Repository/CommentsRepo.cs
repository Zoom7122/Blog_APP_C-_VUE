using blogApp_DAL;
using blogApp_DAL.Model;
using BlogAPP_DAL.Intarface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlogAPP_DAL.Repository
{
    public class CommentsRepo : ICommentsRepo
    {

        private readonly Blog_DBcontext _context;

        public CommentsRepo(Blog_DBcontext context)
        {
            _context = context;
        }

        public async Task AddCommetsToArticle(Comment comment)
        {
            var entry = _context.Entry(comment);
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync();
        }

        public async Task<int> FindCountCommetsWroteByUser(string userId)
        {
            return await _context.Comments.CountAsync(x => x.UserId == userId);
        }
    }
}
