using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Product;

namespace MyMarket.Application.Services.Products.Commands.CreateNewCategory
{
    public class CreateNewCategoryService : ICreateNewCategoryService
    {
        private readonly IMyDBContetx _context;
        public CreateNewCategoryService(IMyDBContetx contetx)
        {
            _context = contetx;
        }
        public ResultDto Execute(long? ParentId, string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا نام دسته بندی را وارد کنید",
                };

            }


            Category category = new Category()
            {
                Name = Name,
                ParentId=ParentId,
                ParentCategory = FindParent(ParentId),
               
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت افزوده شد",
            };
        }

        private Category FindParent(long? parentId)
        {
            return _context.Categories.Find(parentId);

        }



    }
}
