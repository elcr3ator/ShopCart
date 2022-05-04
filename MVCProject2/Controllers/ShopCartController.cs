using Microsoft.AspNetCore.Mvc;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using MVCProject2.ViewModels;

namespace MVCProject2.Controllers
{
    public class ShopCartController : Controller
    {
        private IAllProducts _productRepository;
        private readonly ShopCart _shopCart;
        public ShopCartController(IAllProducts productRepository, ShopCart shopCart)
        {
            _productRepository = productRepository;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            var items = _shopCart.GetShopItems();
            _shopCart.listProductItems = items;

            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart,
            };

            return View(obj);
        }

        public RedirectToActionResult addToCart(int id) {
            var item = _productRepository.products.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        
    }
}
