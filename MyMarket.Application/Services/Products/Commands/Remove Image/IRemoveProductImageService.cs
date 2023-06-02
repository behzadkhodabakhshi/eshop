using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Products.Commands.Remove_Image
{
   public interface IRemoveProductImageService
    {
        ResultDto Execute(long id);
    }


    public class RemoveProductImageService : IRemoveProductImageService
    {
        private readonly IMyDBContetx _context;
        public RemoveProductImageService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(long id)
        {
            var image = _context.GetProductImages.Find(id);
            _context.GetProductImages.Remove(image);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "عکس حذف گردید",
            };

        }
    }
}
