namespace SilverGym.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Products()
        {
            var viewModel = await this.productsService.GetAll();
            return this.View(viewModel);
        }

        public async Task<IActionResult> ProductDetails(string id)
        {
            var viewModel = await this.productsService.GetProduct(id);
            return this.View(viewModel);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await this.productsService.GetCartProducts(userId);
            return this.View(viewModel);
        }
    }
}
