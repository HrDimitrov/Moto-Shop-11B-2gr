namespace MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }   
        public int Quantity { get; set; }

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();

    }
}
