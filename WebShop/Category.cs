using System.ComponentModel.DataAnnotations;

namespace WebShop
{
    public class Category
    {
        public int id { get; set; }
        
        public string title { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
        
        public string image { get; set; } = string.Empty;
        public ICollection<Product> products { get; set; }

    }
}
