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
    public class SessionsController : ControllerBase
    {
        private readonly ISessionDao sessionDAO;

        public SessionsController(ISessionDao _sessionDao)
        {
            sessionDAO = _sessionDao;
        }

        [HttpGet("user/{userID}")]
        public ActionResult<List<Session>> GetAllSessions(int userID)
        {
            List<Session> sessions = sessionDAO.GetAllSessions(userID);
            return sessions;
        }

        [HttpGet("user/{userID}/session/{sessionID}")]
        public ActionResult<Session> GetSessionByID(int userID, int sessionID)
        {
            Session session = sessionDAO.GetSessionByID(userID, sessionID);
            if (session != null)
            {
                return Ok(session);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("user/{userID}")]
        public ActionResult<Card> AddSession(int userID, Session newSession)
        {
            try
            {
                Session created = sessionDAO.AddSession(userID, newSession);
                return Created($"[controller]/createSession/{userID}/{created.SessionID}", created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

       
    }
}
