namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Data.Models.Shopping;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddToCart(string userId, string productId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Моля влезте в профила!");
            }

            var product = await this.db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ArgumentException("Няма такъв продукт!");
            }

            var shoppingCart = await this.db.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(u => u.OwnerId == userId);

            if (shoppingCart == null)
            {
                var cart = new ShoppingCart()
                {
                    Owner = user,
                    OwnerId = user.Id,
                };

                await this.db.ShoppingCarts.AddAsync(cart);
                await this.db.SaveChangesAsync();

                shoppingCart = await this.db.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(u => u.OwnerId == userId);
            }

            var cartProduct = new CartProduct()
            {
                Name = product.Name,
                Price = product.Price,
                MainImage = product.MainImage,
                ShoppingCart = shoppingCart,
                ShoppingCartId = shoppingCart.ShoppingCartId,
                Count = 1,
                Description = product.Description,
            };

            var existingProduct = shoppingCart.Products.FirstOrDefault(p => p.Name == cartProduct.Name
            && p.Price == cartProduct.Price
            && cartProduct.Description == p.Description);

            if (existingProduct != null)
            {
                existingProduct.Count += 1;
                existingProduct.TotalPrice = existingProduct.Price * existingProduct.Count;
            }
            else
            {
                shoppingCart.Products.Add(cartProduct);
                await this.db.CartProducts.AddAsync(cartProduct);
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<ProductViewModel>> GetAll()
        {
            // TODO implement number of items per page when added pages
            return await this.db.Products
                .Where(p => !(p is CartProduct) || ((CartProduct)p).ShoppingCartId == null)
                .Select(p => new ProductViewModel()
                {
                    Name = p.Name,
                    MainImagePath = p.MainImage,
                    Price = p.Price,
                    ProductId = p.ProductId,
                }).ToListAsync();
        }

        public async Task<ICollection<ProductCartViewModel>> GetCartProducts(string userId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Моля влезте в профила!");
            }

            var cart = await this.db.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.OwnerId == user.Id);

            return cart.Products.Select(p => new ProductCartViewModel()
            {
                Name = p.Name,
                Count = p.Count,
                ProductId = p.ProductId,
                TotalPrice = p.Count * p.Price,
                MainImage = p.MainImage,
            }).ToList();
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

        public async Task DecreaseCount(string userId, string productId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Моля влезте в профила!");
            }

            var product = await this.db.CartProducts.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ArgumentException("Няма такъв продукт!");
            }

            var cart = await this.db.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.OwnerId == user.Id);

            var cartProduct = cart.Products.FirstOrDefault(p => p.ProductId == productId);

            if (cartProduct == null)
            {
                throw new ArgumentException("Няма такъв продукт във вашата количка!");
            }

            if (cartProduct.Count <= 1)
            {
                throw new ArgumentException("Количеството не може а е по-малко от 1.");
            }

            cartProduct.Count -= 1;
            cartProduct.TotalPrice = cartProduct.Price * cartProduct.Count;

            await this.db.SaveChangesAsync();
        }

        public async Task IncreaseCount(string userId, string productId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Моля влезте в профила!");
            }

            var product = await this.db.CartProducts.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ArgumentException("Няма такъв продукт!");
            }

            var cart = await this.db.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.OwnerId == user.Id);

            var cartProduct = cart.Products.FirstOrDefault(p => p.ProductId == productId);

            if (cartProduct == null)
            {
                throw new ArgumentException("Няма такъв продукт във вашата количка!");
            }

            cartProduct.Count += 1;
            cartProduct.TotalPrice = cartProduct.Price * cartProduct.Count;

            await this.db.SaveChangesAsync();
        }
    }
}
