namespace ShopGI.Models.ViewModels
{
    public class ProductLVModel
    {
        public IEnumerable<Product> Product { get; set; }
        public string CurrentCategory { get; set; }
    }
}
