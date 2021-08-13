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
    public class SessionTypesController : ControllerBase
    {
        private readonly ISessionTypeDao typeDAO;

        public SessionTypesController(ISessionTypeDao _typeDao)
        {
            typeDAO = _typeDao;
        }

        [HttpGet]
        public ActionResult<List<SessionType>> GetAllTags()
        {
            List<SessionType> types = typeDAO.GetAllSessionTypes();
            return types;
        }

        [HttpGet("{sessionTypeID}")]
        public ActionResult<SessionType> GetSessionTypeByID(int sessionTypeID)
        {
            SessionType type = typeDAO.GetSessionTypeByID(sessionTypeID);
            if (type != null)
            {
                return Ok(type);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("name/{sessionTypeName}")]
        public ActionResult<SessionType> GetSessionTypeByName(string sessionTypeName)
        {
            SessionType type = typeDAO.GetSessionTypeByName(sessionTypeName);
            if (type != null)
            {
                return Ok(type);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
