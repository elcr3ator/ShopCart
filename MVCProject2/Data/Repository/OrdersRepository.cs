using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;

namespace MVCProject2.Data.Repository
{
    public class OrdersRepository : IAllOrders
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;

        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;   
        }

        public void createOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            appDBContent.order.Add(order);
            appDBContent.SaveChanges();
            var items = shopCart.listProductItems;
            foreach (var item in items)
            {
                var orderDetail = new OrderDetail()
                {
                    ProductID = item.product.id,
                    OrderID = order.Id,
                    Price = item.product.price
                };
                appDBContent.orderDetail.Add(orderDetail);

            }
            appDBContent.SaveChanges();
        }
    }
}
