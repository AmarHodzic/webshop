namespace WebShop
{
    public class OrderDTO
    {
        public string[] quantities { get; set; }
        public string state { get; set; }
        public string adress { get; set; }
        public int price { get; set; }
        public int[] products { get; set; }

    }
}
