using MVCProject2.Data.Models;

namespace MVCProject2.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> GetAllProducts { get; set; }

        public IEnumerable<Product> getAllProduct { get; set; }
        public string currentCategory { get; set; }
    }
}
