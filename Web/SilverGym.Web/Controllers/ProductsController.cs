namespace SilverGym.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Products;

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
            var viewModel = new List<ProductCartViewModel>();

            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                viewModel = (await this.productsService.GetCartProducts(userId)).ToList();
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("product", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddToCart(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.productsService.AddToCart(userId, id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("product", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("ShoppingCart");
            }

            return this.Redirect("/Products/ShoppingCart");
        }

        public async Task<IActionResult> IncreaseCount(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.productsService.IncreaseCount(userId, id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("product", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("ShoppingCart");
            }

            return this.Redirect("/Products/ShoppingCart");
        }

        public async Task<IActionResult> DecreaseCount(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.productsService.DecreaseCount(userId, id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("product", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("ShoppingCart");
            }

            return this.Redirect("/Products/ShoppingCart");
        }
    }
}
