using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interface.Repository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStockAsync(QueryObject filterObject);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock?> CreateStockAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(int id, UpdateRequestStockDto updateRequestStockDto);
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> IsExistAsync(int id);
    }
}