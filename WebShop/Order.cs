using System.ComponentModel.DataAnnotations;

namespace WebShop
{
    public class Order
    {
        public int id { get; set; }

        //public ICollection<Product> products { get; set; }
        //public ICollection<ProductOrders> productsOrders { get; set; }
        public string[] quantities { get; set; }
        
        [StringLength(20)]
        public string state { get; set; } = string.Empty;
        public string adress { get; set; } = string.Empty;
        public int price { get; set; }

        public Order()
        {
           // products = new HashSet<Product>();
            quantities = new string[20]; 
        }
    }
}
