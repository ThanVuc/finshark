using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interface.Repository;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStockRepository _stockRepos;

        public StockController(AppDbContext appDbContext, IStockRepository stockRepository)
        {
            _context = appDbContext;
            _stockRepos = stockRepository;
        }

        // List of Stocks
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]QueryObject query){           
            var stocks = await _stockRepos.GetAllStockAsync(query);

            var stockDtos = stocks.Select(s => s.ToStockDto());

            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail([FromRoute]int id){

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stock = await _stockRepos.GetStockByIdAsync(id);
            
            if (stock == null){
                return NotFound();
            }
            
            return Ok(stock.ToStockDetailDto());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateStock([FromBody] CreateRequestStockDto createStockDto){
            
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            
            var stockModel = createStockDto.FromCreateStockDtoToStock();
            
            stockModel = await _stockRepos.CreateStockAsync(stockModel);

            if (stockModel == null){
                return NotFound();
            }
            
            return CreatedAtAction(nameof(Index),new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateRequestStockDto updateRequestStockDto){
            
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepos.UpdateStockAsync(id,updateRequestStockDto);
            
            if (stockModel == null){
                return NotFound();
            }

            
            return Ok(stockModel.ToStockDto());
        }
    
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id){
            
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stock = await _stockRepos.DeleteStockAsync(id);

            if (stock == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}