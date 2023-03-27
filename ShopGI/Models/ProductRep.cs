namespace ShopGI.Models
{
    public class ProductRep : IProductRep
    {
        AppDbContext context { get; }
        public ProductRep(AppDbContext context)
        {
            this.context = context;
        }
        public List<Product> GetAllProduct()
        {
            return context.Product.ToList();
        }
        public Product GetProduct(int id)
        {
            return context.Product.Find(id);
        }
        public void Update(Product product)
        {
            context.Product.Update(product);
            context.SaveChanges();
        }

        public void Add(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Product.Remove(GetProduct(id));
            context.SaveChanges();
        }
    }
}
