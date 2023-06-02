using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Products.Commands.Remove_Feature
{
   public interface IRemoveProductFeatureService
    {
        ResultDto Execute(long id);
    }

    public class RemoveProductFeatureService : IRemoveProductFeatureService
    {
        private readonly IMyDBContetx _context;
        public RemoveProductFeatureService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(long id)
        {
            var feature = _context.ProductFeatures.Find(id);
            _context.ProductFeatures.Remove(feature);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویژگی حذف گردید",
            };

        }
    }
}
