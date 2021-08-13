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
    public class TagsController : ControllerBase
    {
        private readonly ITagDao tagDAO;

        public TagsController(ITagDao _tagDao)
        {
            tagDAO = _tagDao;
        }

        [HttpGet]
        public ActionResult<List<Tag>> GetAllTags()
        {
            List<Tag> tags = tagDAO.GetAllTags();
            return tags;
        }

        [HttpGet("{tagID}")]
        public ActionResult<Tag> GetTagByID(int tagID)
        {
            Tag tag = tagDAO.GetTagByID(tagID);
            if (tag != null)
            {
                return Ok(tag);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("name/{tagName}")]
        public ActionResult<Tag> GetCategoryByName(string tagName)
        {
           Tag tag = tagDAO.GetTagByName(tagName);
            if (tag != null)
            {
                return Ok(tag);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
