namespace SilverGym.Data.Models.Shopping
{
    public class CartProduct : Product
    {
        public int Count { get; set; }

        public double TotalPrice { get; set; }

        public virtual string ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
