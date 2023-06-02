using System;
using System.Collections.Generic;
using System.Text;
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
using MyMarket.Application.Services.Products.Queries.GetProductFullCategory;

namespace MyMarket.Application.Interfaces.FacadPattens
{
    public interface IProductFacad
    {
        ICreateNewCategoryService CreateNewCategoryService { get; }

        IGetAllCategoriesService GetAllCategoriesService { get; }

        IGetMenuItem GetGetMenuItem { get; }

        IGetFullCategory GetFullCategory {get; }

   
        ICreateNewProductService CreateNewProductService { get; }
        IEditProductService EditProductService { get; }


        IRemoveProductFeatureService RemoveProductFeatureService { get; }

        IGetAllProductForSiteService GetAllProductForSiteService { get; }

        //IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }

        IGetSearchCategoryService GetSearchCategoryService { get; }

        IRemoveProductImageService RemoveProductImageService { get; }


        IPEditProductService PEditProductService { get; }



    }
}
