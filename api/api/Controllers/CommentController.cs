using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interface.Repository;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepos;
        private readonly IStockRepository _stockRepos;

        public CommentController(ICommentRepository comment, IStockRepository stock)
        {
            _commentRepos = comment;
            _stockRepos = stock;
        }

        [HttpGet("comment")]
        public async Task<IActionResult> Index(){
            var allComments = (await _commentRepos.GetAllCommentAsync())
            .Select(cmt => cmt.ToCommentDto());

            return Ok(allComments);
        }

        [HttpGet("comment/detail/{id:int}")]
        public async Task<IActionResult> GetCommentById(int id){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepos.GetCommentByIdAsync(id);

            if (comment == null){
                return NotFound();
            }

            return Ok(comment.ToCommentDetailDto());
        }

        [HttpGet("stock/{stocckId:int}/comment")]
        public async Task<IActionResult> GetCommentByStockId(int stocckId){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if (!await _stockRepos.IsExistAsync(stocckId)){
                return NotFound();
            }

            var comments = (await _commentRepos.GetCommentByStockIdAsync(stocckId))
            .Select(cmt => cmt.ToCommentDto());


            return Ok(comments);
        }

        [HttpPost("stock/{stockId:int}/comment/create")]
        public async Task<IActionResult> CreateComment([FromRoute]int stockId,
        [FromBody] CreateCommentRequestDto createCommentDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if (!await _stockRepos.IsExistAsync(stockId)){
                return BadRequest("Stock does not exists");
            }

            var comment = createCommentDto.CreateCommentRequestDtoToComment();

            comment = await _commentRepos.CreateCommentAsync(stockId,comment);

            if (comment == null){
                return BadRequest("Comment is invaild");
            }

            return CreatedAtAction(nameof(GetCommentById),new {id = comment.Id}, comment.ToCommentDetailDto());

        }

        [HttpPut("comment/update/{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, 
        [FromBody] UpdateCommentRequestDto updateCommentRequestDto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var commentModel = updateCommentRequestDto.UpdateCommentRequestDtoToComment();
            commentModel = await _commentRepos.UpdateCommentAsync(id,commentModel);

            if (commentModel == null){
                return NotFound();
            }

            return Ok(commentModel);
        }

        [HttpDelete("comment/delete/{id:int}")]
        public async Task<IActionResult> DeleteCommet([FromRoute] int id){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }


            var comment = await _commentRepos.DeleteCommentAsync(id);
            if (comment == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}