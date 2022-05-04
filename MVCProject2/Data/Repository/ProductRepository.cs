using Microsoft.EntityFrameworkCore;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;

namespace MVCProject2.Data.Repository
{
    public class ProductRepository : IAllProducts
    {
        private readonly AppDBContent appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Product> products => appDBContent.product.Include(c => c.Category);
        
        public IEnumerable<Product> getFavProducts => appDBContent.product.Where(p => p.isFavourite).Include(c => c.Category);
        
        public Product getObjectProduct(int productId) => appDBContent.product.FirstOrDefault(p => p.id == productId);

        public IEnumerable<Product> getAllProduct => appDBContent.product.OrderBy(p => p.id);
        public void UpdateOrderFalse(Product product)
        {
            product.isFavourite = true;
            this.appDBContent.product.Update(product);

            appDBContent.SaveChanges();
        }

        public void UpdateOrderTrue(Product product)
        {
            product.isFavourite = false;
          this.appDBContent.product.Update(product);

            appDBContent.SaveChanges();
        }
    }
}
