namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        public ICollection<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductDetailsViewModel GetProduct(string productId)
        {
            throw new NotImplementedException();
        }
    }
}
