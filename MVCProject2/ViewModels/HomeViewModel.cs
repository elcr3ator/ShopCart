using MVCProject2.Data.Models;

namespace MVCProject2.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> favProducts { get; set; }
    }
}
