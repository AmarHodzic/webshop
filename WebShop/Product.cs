using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop
{
    public class Product
    {
        public int id { get; set; }

        [StringLength(20)]
        public string title { get; set; } = string.Empty;
        [StringLength(200)]
        public string desc { get; set; } = string.Empty;
        public int price { get; set; }
        public int quantity { get; set; }

        public string[] images { get; set; }

        //[JsonIgnore]
        //public ICollection<Order> orders { get; set; }
        //public ICollection<ProductOrders> productsOrders { get; set; }
        //public int catId { get; set; }
        //public Category? Category { get; set; }

        public Product()
        {
            //orders = new HashSet<Order>();
            images = new string[20];
        }
    }
}
