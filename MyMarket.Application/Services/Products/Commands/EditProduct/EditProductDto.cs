using Microsoft.AspNetCore.Http;
using MyMarket.Application.Services.Products.Commands.CreateNewProduct;
using System.Collections.Generic;

namespace MyMarket.Application.Services.Products.Commands.EditProduct
{
    public class EditProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; }
        public bool Displayed { get; set; }
        public bool InstantOffer { get; set; }


        public List<IFormFile> Images { get; set; }
        public List<ProductFeaturesDto> productFeatures { set; get; }

    }
}

