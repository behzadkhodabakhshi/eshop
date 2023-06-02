using Microsoft.AspNetCore.Hosting;
using MyMarket.Application.Interfaces;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.Common.GetMenuItem;
using MyMarket.Application.Services.Common.GetSearchCategory;
using MyMarket.Application.Services.Products.Commands.CreateNewCategory;
using MyMarket.Application.Services.Products.Commands.CreateNewProduct;
using MyMarket.Application.Services.Products.Commands.EditProduct;
using MyMarket.Application.Services.Products.Commands.Remove_Feature;
using MyMarket.Application.Services.Products.Commands.Remove_Image;
using MyMarket.Application.Services.Products.Queries.GetAllCategories;
using MyMarket.Application.Services.Products.Queries.GetAllProductForSite;
using MyMarket.Application.Services.Products.Queries.GetFullCategory;
using MyMarket.Application.Services.Products.Queries.GetProductDetailsForSite;
using System;

namespace MyMarket.Application.Services.Products.FacadPatterns
{
    public class ProductFacad : IProductFacad
    {

        private readonly IMyDBContetx _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IMyDBContetx context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;

        }
        private ICreateNewCategoryService _CreateNewCategory;
        public ICreateNewCategoryService CreateNewCategoryService
        {
            get
            {
                return _CreateNewCategory = _CreateNewCategory ?? new CreateNewCategoryService(_context);
            }
        }

        private IGetAllCategoriesService _GetCategoriesService;

        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _GetCategoriesService = _GetCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }

        private IGetMenuItem _GetMenuItem;

        public IGetMenuItem GetGetMenuItem
        {
            get
            {
                return _GetMenuItem = _GetMenuItem ?? new GetMenuItem(_context);
            }
        }

        private IGetFullCategory _GetFullCategory;

        public IGetFullCategory GetFullCategory
        {
            get
            {
                return _GetFullCategory = _GetFullCategory ?? new GetFullCategory(_context);
            }
        }





        private ICreateNewProductService _CreateNewProductService;
        public ICreateNewProductService CreateNewProductService
        {
            get
            {
                return _CreateNewProductService = _CreateNewProductService ?? new CreateNewProductService(_context, _environment);
            }
        }

        private IEditProductService _EditProductService;
        public IEditProductService EditProductService
        {
            get
            {
                return _EditProductService = _EditProductService ?? new EditProductService(_context);
            }
        }

        private IRemoveProductFeatureService _RemoveProductFeatureService;
        public IRemoveProductFeatureService RemoveProductFeatureService
        {
            get
            {
                return _RemoveProductFeatureService = _RemoveProductFeatureService ?? new RemoveProductFeatureService(_context);
            }
        }


        private IRemoveProductImageService _RemoveProductImageService;
        public IRemoveProductImageService RemoveProductImageService
        {
            get
            {
                return _RemoveProductImageService = _RemoveProductImageService ?? new RemoveProductImageService(_context);
            }
        }

        private IPEditProductService _PEditProductService;
        public IPEditProductService PEditProductService
        {
            get
            {
                return _PEditProductService = _PEditProductService ?? new PEditProductService(_context, _environment);
            }
        }

        private IGetAllProductForSiteService _getAllProductForSiteService;
        public IGetAllProductForSiteService GetAllProductForSiteService
        {
            get
            {
                return _getAllProductForSiteService = _getAllProductForSiteService ?? new GetAllProductForSiteService(_context);
            }
        }



        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(_context);
            }
        }

        private IGetSearchCategoryService _getSearchCategoryService;

        public IGetSearchCategoryService GetSearchCategoryService
        {
            get
            {
                return _getSearchCategoryService = _getSearchCategoryService ?? new GetSearchCategoryService(_context);
            }
        }




    }
}
