using Microsoft.EntityFrameworkCore;

namespace MVCProject2.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent appDBContent;

        public ShopCart(AppDBContent AppDBContent)
        {
            this.appDBContent = AppDBContent;
        }
        public string ShopCartId { get; set; }
        public List<ShopProductItem> listProductItems { get; set; }

        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }

        public void AddToCart(Product product)
        {
            this.appDBContent.shopProductItem.Add(new ShopProductItem
            {
                ShopCartId = ShopCartId, 
                product = product,
                price = product.price
            });

            appDBContent.SaveChanges();
        }

        public List<ShopProductItem> GetShopItems()
        {
            return appDBContent.shopProductItem.Where(c => c.ShopCartId == ShopCartId).Include(s => s.product).ToList();
        }
    }
}
