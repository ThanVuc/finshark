using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentAsync();
        Task<List<Comment>> GetCommentByStockIdAsync(int id);
        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment?> CreateCommentAsync(int stockId, Comment commentModel);

        Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);

        Task<Comment?> DeleteCommentAsync(int id);


    }
}