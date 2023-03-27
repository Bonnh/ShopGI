namespace ShopGI.Models
{
    public class AddViewModel
    {
        public int ID { get; set; }
        public string AR { get; set; }
        public string Server { get; set; }
        public float Champ { get; set; }
        public string Desccription { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public IFormFile PhotoPath { get; set; }
    }
}
