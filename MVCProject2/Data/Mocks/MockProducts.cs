using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Data.Mocks
{
    public class MockProducts : IAllProducts
    {
        private readonly IProductsCategory _productsCategory = new MockCategory();
        public IEnumerable<Product> products {
            get
            {
                return new List<Product> {
                new Product { name = "Assault rifle",
                    shortDesc ="Automated rifle for self-defense or offense",
                    longDesc ="A powerful modern rifle which is used in modern conflicts for any military actions",
                    img = "/img/haenel-mk556-vollautomat-gruen-links.png",
                    price =17000,
                    isFavourite = true,
                    available = true,
                    Category = _productsCategory.categories.First()
                },
                new Product
                {
                    name = "FIM-92 Stinger",
                    shortDesc = "man-portable air-defense system",
                    longDesc = "It can be adapted to fire from a wide variety of ground vehicles and helicopters",
                    img = "/img/pzrk-quotstingerquot-harakteristiki-i-sravnenie-s-analogami.jpg",
                    price = 45000,
                    isFavourite = true,
                    available = true,
                    Category = _productsCategory.categories.First()
                },
                new Product
                {
                    name = "FGM-148 Javelin",
                    shortDesc = "American-made portable anti-tank missile",
                    longDesc = "Its fire-and-forget design uses automatic infrared guidance that allows the user to seek cover immediately after launch, as opposed to wire-guided systems",
                    img = "/img/450px-Javelin_with_checkout_equipment.jpg",
                    price = 38000,
                    isFavourite = true,
                    available = true,
                    Category = _productsCategory.categories.First()
                },
                new Product
                {
                    name = "Bulletproof",
                    shortDesc = "Protect from injuries in body",
                    longDesc = "This thing is required for every military as it can save your life",
                    img = "/img/bulletproof_vest_PNG93769.png",
                    price = 8500,
                    isFavourite = true,
                    available = true,
                    Category = _productsCategory.categories.Last()
                },
                new Product
                {
                    name = "Helmet",
                    shortDesc = "Protect from injuries in head",
                    longDesc = "This thing is required for every military as it can save your life",
                    img = "/img/kisspng-combat-helmet-fast-helmet-helmet-cover-lightweight-fast-helmet-ams-728-umbrella-international-5b67277214f584.2599966215334869620859.jpg",
                    price = 6200,
                    isFavourite = true,
                    available = true,
                    Category = _productsCategory.categories.Last()
                }
            };
            }
        }
        public IEnumerable<Product> getFavProducts { get; set; }

        public IEnumerable<Product> getAllProduct {get; }

        public Product getObjectProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderFalse(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderTrue(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
