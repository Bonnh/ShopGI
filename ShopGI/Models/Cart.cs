namespace ShopGI.Models
{
    public class Cart
    {
        private List<CardItem> ItemCollection = new List<CardItem>();
        public virtual IEnumerable<CardItem> Items => ItemCollection;
        public virtual void AddItem(Product product, int quantity)
        {
            CardItem item = ItemCollection.Where(p => p.Product.ID==product.ID).FirstOrDefault();
            if (item != null)
            {
                ItemCollection.Add(new CardItem { Product = product, Quantity = quantity});
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
    public class CardItem
    {
        public int CardItemID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
