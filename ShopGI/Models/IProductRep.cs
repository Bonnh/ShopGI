namespace ShopGI.Models
{
    public interface IProductRep
    {
        
        public Product GetProduct(int id);

        public List<Product> GetAllProduct();

        public void Update(Product product);

        public void Add(Product product);

        public void Delete(int id);
    }
}
