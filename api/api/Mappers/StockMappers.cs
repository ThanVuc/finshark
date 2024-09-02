using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){
            return new StockDto(){
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                Purchase = stockModel.Purchase,
                MarketCap = stockModel.MarketCap,
                Symbol = stockModel.Symbol
            };
        }

        public static StockDetailDto ToStockDetailDto(this Stock stockModel){
            return new StockDetailDto(){
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                Purchase = stockModel.Purchase,
                MarketCap = stockModel.MarketCap,
                Symbol = stockModel.Symbol,
                Comments = stockModel.Comments
            };
        }
        public static Stock FromCreateStockDtoToStock(this CreateRequestStockDto createStockDto){
            return new Stock(){
                CompanyName = createStockDto.CompanyName,
                Industry = createStockDto.Industry,
                LastDiv = createStockDto.LastDiv,
                Purchase = createStockDto.Purchase,
                MarketCap = createStockDto.MarketCap,
                Symbol = createStockDto.Symbol
            };
        }
    }
}