namespace MVCProject2.Data.Models
{
    public class ShopProductItem
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int price { get; set; }
        public string ShopCartId { get; set; }
    }
}
