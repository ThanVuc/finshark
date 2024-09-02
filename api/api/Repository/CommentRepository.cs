using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface.Repository;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Comment?> CreateCommentAsync(int stockId, Comment commentModel)
        {

            if (commentModel == null){
                return null;
            }

            commentModel.StockId = stockId;
            await _context.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null){
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllCommentAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
            .Include(cmt => cmt.Stock)
            .FirstOrDefaultAsync(cmt => cmt.Id == id);
            return comment;
        }

        public async Task<List<Comment>> GetCommentByStockIdAsync(int id)
        {
            var comments = await _context.Comments
            .Include(cmt => cmt.Stock)
            .Where(cmt => cmt.StockId == id)
            .ToListAsync();
            return comments;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var existedComment = await _context.Comments.FindAsync(id);

            if (existedComment == null){
                return null;
            }

            existedComment.Title = commentModel.Title;
            existedComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existedComment;
        }
    }
}