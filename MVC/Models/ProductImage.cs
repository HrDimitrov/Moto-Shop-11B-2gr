using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{

    [Table("productimages")]
     
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        public Product Product { get; set; }
    }
}
