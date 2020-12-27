namespace SilverGym.Data.Models.Shopping
{
    using System;
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.ShoppingCartId = Guid.NewGuid().ToString();
            this.Products = new HashSet<CartProduct>();
        }

        public string ShoppingCartId { get; set; }

        public virtual string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; }
    }
}