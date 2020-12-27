namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<ProductViewModel>> GetAll() 
        {
            // TODO implement number of items per page when added pages
            return await this.db.Products.Select(p => new ProductViewModel()
            {
                Name = p.Name,
                MainImagePath = p.MainImage,
                Price = p.Price,
                ProductId = p.ProductId,
            }).ToListAsync();
        }

        public async Task<ProductDetailsViewModel> GetProduct(string productId)
        {
            var product = await this.db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ArgumentException("Няма такъв продукт!");
            }

            return new ProductDetailsViewModel()
            {
                Description = product.Description,
                MainImagePath = product.MainImage,
                Price = product.Price,
                ProductId = product.ProductId,
                Name = product.Name,
            };
        }
    }
}
