using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDao categoryDAO;

        public CategoriesController(ICategoryDao _categoryDao)
        {
            categoryDAO = _categoryDao;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            List<Category> categories = categoryDAO.GetCategories();
            return categories;
        }

        [HttpGet("{categoryID}")]
        public ActionResult<Category> GetCategoryByID(int categoryID)
        {
            Category category = categoryDAO.GetCategoryByID(categoryID);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("name/{categoryName}")]
        public ActionResult<Category> GetCategoryByName(string categoryName)
        {
            Category category = categoryDAO.GetCategoryByName(categoryName);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
