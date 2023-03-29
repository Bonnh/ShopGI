namespace ShopGI.Models
{
    public class Cart : ICart
    {
        private List<CartItem> ItemCollection = new List<CartItem>();
        public virtual IEnumerable<CartItem> Items => ItemCollection;
        public virtual void AddItem(Product product, int quantity)
        {
            CartItem item = ItemCollection.Where(p => p.Product.ID==product.ID).FirstOrDefault();
            if (item == null)
            {
                ItemCollection.Add(new CartItem { Product = product, Quantity = quantity});
            }
            else
            {
                item.Quantity = quantity;
            }
        }
        public virtual void RemoveItem(Product product) => ItemCollection.RemoveAll(l => l.Product.ID == product.ID);
        public virtual double ComputeTotalValue()=> ItemCollection.Sum(l => l.Product.Price * l.Quantity);

        public virtual void Clear() => ItemCollection.Clear();
    }
    public class CartItem
    {
        public int CartItemID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
