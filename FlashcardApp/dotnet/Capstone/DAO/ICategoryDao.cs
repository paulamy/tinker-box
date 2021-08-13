using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICategoryDao
    {
        List<Category> GetCategories();

        Category GetCategoryByID(int categoryID);

        Category GetCategoryByName(string name);
    }
}
