using Microsoft.AspNetCore.Mvc;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using MVCProject2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAllProducts allProducts;
        private readonly IProductsCategory allCategory;

        public ProductsController(IAllProducts iAllProducts, IProductsCategory iProductsCategory)
        {
            allProducts = iAllProducts;
            allCategory = iProductsCategory;
        }
        [Route("Products/List")]
        [Route("Products/List/{category}")]
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category))
            {
                products = allProducts.products.OrderBy(i => i.id);
            }
            else
            {
                if(string.Equals("Military", category, StringComparison.OrdinalIgnoreCase))
                {
                    products = allProducts.products.Where(i => i.Category.CategoryName.Equals("Military")).OrderBy(i => i.id);
                    currCategory = "Military";
                }
                else if (string.Equals("Supplies", category, StringComparison.OrdinalIgnoreCase)){
                    products = allProducts.products.Where(i => i.Category.CategoryName.Equals("Supplies")).OrderBy(i => i.id);
                    currCategory = "Supplies";
                }

                

               
            }

            ViewBag.Title = "Page with military product";
            var productObj = new ProductsListViewModel
            {
                GetAllProducts = products,
                currentCategory = currCategory
            };
            return View(productObj);
        }

        public RedirectResult setFavourite(int id)
        {
            var item = allProducts.products.FirstOrDefault(i => i.id == id);
            if (item.isFavourite == true)
            {
                allProducts.UpdateOrderTrue(item);
            }
            else
            {
                allProducts.UpdateOrderFalse(item);
            }
            return new RedirectResult("/Products/List");
        }
    }
}
