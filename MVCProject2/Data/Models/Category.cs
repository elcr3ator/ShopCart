using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Data.Models
{
    public class Category
    {
        public int Id { set; get; }
        public string CategoryName { set; get; }
        public string Description { set; get; }
        public List<Product> Products { set; get; }
    }
}
