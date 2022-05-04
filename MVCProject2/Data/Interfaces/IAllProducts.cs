using MVCProject2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Data.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> products { get; }
        IEnumerable<Product> getFavProducts { get; }
        Product getObjectProduct (int productId);

        IEnumerable<Product> getAllProduct { get; }
        void UpdateOrderTrue(Product product);

        void UpdateOrderFalse(Product product);
    }
}
