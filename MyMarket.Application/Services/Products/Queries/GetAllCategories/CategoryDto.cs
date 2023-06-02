namespace MyMarket.Application.Services.Products.Queries.GetAllCategories
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

       

        public bool HasChild { get; set; }

        public ParentCategoryDto Parent { get; set; }

        
    }

    public class ParentCategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public ParentparentCategoryDto Parent_parent { get; set; }
    }


    public class ParentparentCategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    
    }

}
