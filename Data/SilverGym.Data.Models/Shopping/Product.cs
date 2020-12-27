namespace SilverGym.Data.Models.Shopping
{
    using System;

    public class Product
    {
        public Product()
        {
            this.ProductId = Guid.NewGuid().ToString();
        }

        public string ProductId { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MainImage { get; set; }
    }
}
