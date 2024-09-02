using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel){
            return new CommentDto(){
                Id = commentModel.Id,
                Content = commentModel.Content,
                OnCreate = commentModel.OnCreate,
                StockId = commentModel.StockId
            };
        }

        public static CommentDetailDto ToCommentDetailDto(this Comment commentModel){
            return new CommentDetailDto(){
                Id = commentModel.Id,
                Content = commentModel.Content,
                OnCreate = commentModel.OnCreate,
                StockId = commentModel.StockId,
                Stock = commentModel.Stock
            };
        }

        public static Comment CreateCommentRequestDtoToComment(this CreateCommentRequestDto createCommentDto){
            return new Comment(){
                Title = createCommentDto.Title,
                Content = createCommentDto.Content
            };
        }

        public static Comment UpdateCommentRequestDtoToComment(this UpdateCommentRequestDto updateCommentRequestDto){
            return new Comment(){
                Title = updateCommentRequestDto.Title,
                Content = updateCommentRequestDto.Content
            };
        }

    }
}