namespace SilverGym.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile MainImage { get; set; }
    }
}
