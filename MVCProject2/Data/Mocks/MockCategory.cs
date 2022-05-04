using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Data.Mocks
{
    public class MockCategory : IProductsCategory
    {
        public IEnumerable<Category> categories
        { get
            {
                return new List<Category>
                {
                    new Category {CategoryName = "Military", Description = "Products which are used for military purposes"},
                    new Category {CategoryName ="Supplies", Description ="Products which are used to help military"}
                };
            }

        }

    }
}
