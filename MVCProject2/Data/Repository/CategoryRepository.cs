using Microsoft.EntityFrameworkCore;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using System;

namespace MVCProject2.Data.Repository
{
    public class CategoryRepository : IProductsCategory
    {
        private readonly AppDBContent appDBContent;

        public CategoryRepository(AppDBContent AppDBContent)
        {
            this.appDBContent = AppDBContent;
        }
        public IEnumerable<Category> categories => appDBContent.category;

       
    }
}
