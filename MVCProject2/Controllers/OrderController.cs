using Microsoft.AspNetCore.Mvc;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;

namespace MVCProject2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders allOrders;
        private readonly ShopCart shopCart;

        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            shopCart.listProductItems = shopCart.GetShopItems();

            if(shopCart.listProductItems.Count == 0)
            {
                ModelState.AddModelError("", "No products have been selected");
            }

            else 
            {
                allOrders.createOrder(order);
                return Redirect("Complete");
            }
            return View(order);
        }

        public ActionResult Complete()
        {
            ViewBag.Message = "Your order has been successfully added";
            return View();
        }
    }
}
