namespace SilverGym.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SilverGym.Web.ViewModels.Products;

    public interface IProductsService
    {
        public ICollection<ProductViewModel> GetAll();

        public ProductDetailsViewModel GetProduct(string productId);
    }
}
