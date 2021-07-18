using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO userDAO;

        public UsersController(IUserDAO _userDAO)
        {

           userDAO = _userDAO;
        }

        [HttpGet]
        public ActionResult<List<DisplayUser>> GetAllUsers()
        {
            List<DisplayUser> users = userDAO.GetUsers();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<DisplayUser> GetUserById(int id)
        {
            DisplayUser user = userDAO.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
