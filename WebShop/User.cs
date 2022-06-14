using System.ComponentModel.DataAnnotations;

namespace WebShop
{
    public class User
    {
        public int id { get; set; }

        [StringLength(20)]
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public ICollection<Order> orders { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }


    }
}
