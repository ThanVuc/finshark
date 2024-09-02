using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interface.Repository;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> CreateStockAsync(Stock stockModel)
        { 
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null){
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllStockAsync(QueryObject query)
        {
            var stocks = _context.Stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Company)){
                stocks = stocks.Where(s => s.CompanyName.Contains(query.Company)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(query.Industry)){
                stocks = stocks.Where(s => s.Industry.Contains(query.Industry)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy)){
                if (query.SortBy.Equals("symbol",StringComparison.OrdinalIgnoreCase)){
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipPage = (query.PageCurrent - 1)*query.PageSize;

            Console.WriteLine("Skip Page: " + skipPage);

            return await stocks
            .Skip(skipPage)
            .Take(query.PageSize)
            .ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
            .Include(s => s.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Stocks.FindAsync(id) != null;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateRequestStockDto updateRequestStockDto)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null){
                return null;
            }

            stockModel.CompanyName = updateRequestStockDto.CompanyName;
            stockModel.Industry = updateRequestStockDto.Industry;
            stockModel.LastDiv = updateRequestStockDto.LastDiv;
            stockModel.Purchase = updateRequestStockDto.Purchase;
            stockModel.MarketCap = updateRequestStockDto.MarketCap;
            stockModel.Symbol = updateRequestStockDto.Symbol;

            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}