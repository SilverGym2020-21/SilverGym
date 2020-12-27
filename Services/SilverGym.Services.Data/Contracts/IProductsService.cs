namespace SilverGym.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SilverGym.Web.ViewModels.Products;

    public interface IProductsService
    {
        public Task<ICollection<ProductViewModel>> GetAll();

        public Task<ProductDetailsViewModel> GetProduct(string productId);

        public Task<ICollection<ProductCartViewModel>> GetCartProducts(string userId);
    }
}
