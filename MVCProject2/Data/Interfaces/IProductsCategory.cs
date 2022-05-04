using MVCProject2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject2.Data.Interfaces
{
    public interface IProductsCategory
    {
        IEnumerable<Category> categories { get; }
    }
}
