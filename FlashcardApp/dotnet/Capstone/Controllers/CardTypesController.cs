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
    public class CardTypesController : ControllerBase
    {
        private readonly ICardTypeDao typeDAO;

        public CardTypesController(ICardTypeDao _typeDao)
        {
            typeDAO = _typeDao;
        }

        [HttpGet]
        public ActionResult<List<CardType>> GetAllCardTypes()
        {
            List<CardType> types = typeDAO.GetAllCardTypes();
            return types;
        }

        [HttpGet("{cardTypeID}")]
        public ActionResult<SessionType> GetCardTypeByID(int cardTypeID)
        {
            CardType type = typeDAO.GetCardTypeByID(cardTypeID);
            if (type != null)
            {
                return Ok(type);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("name/{cardTypeName}")]
        public ActionResult<SessionType> GetCardTypeByName(string cardTypeName)
        {
            CardType type = typeDAO.GetCardTypeByName(cardTypeName);
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
