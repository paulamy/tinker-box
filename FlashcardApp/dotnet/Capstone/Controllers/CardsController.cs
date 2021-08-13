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
    public class CardsController : ControllerBase
    {
        private readonly ICardDao cardDAO;

        public CardsController(ICardDao _cardDao)
        {
            cardDAO = _cardDao;
        }
        [HttpGet]
        public ActionResult<List<Card>> GetPublicCards()
        {
            List<Card> cards = cardDAO.GetPublicCards();
            return cards;
        }

        [HttpGet("tag/{tag}")]
        public ActionResult<List<Card>> GetPublicCardsByTag(string tag)
        {
            List<Card> cards = cardDAO.GetPublicCardsByTag(tag);
            return cards;
        }

        [HttpGet("user/{userID}/card/{cardID}")]
        public ActionResult<Card> GetCardByID(int userID, int cardID)
        {
            Card card = cardDAO.GetCardByID(userID, cardID);
            if (card != null)
            {
                return Ok(card);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("user/{userID}/deck/{deckID}")]
        public ActionResult<Card> AddCard(int userID, int deckID, Card newCard)
        {

                try
                {
                    Card created = cardDAO.AddCard(userID, deckID, newCard);
                    return Created($"[controller]/createCard/{userID}/{deckID}/{created.CardID}", created);
                }
                catch (System.Exception e)
                {
                    return BadRequest(e);
                }
        }

        [HttpPut("user/{userID}/card/{cardID}")]
        public ActionResult<Card> ModifyCard(int userID, int cardID, Card cardToEdit)
        {

                Card pending = cardDAO.GetCardByID(userID, cardID);
                if (pending == null)
                {
                    return NotFound("Card does not exist");
                }
                else if (cardID != pending.CardID)
                {
                    return NotFound("Card does not exist");
                }
                Card updated = cardDAO.ModifyCard(userID, cardID, cardToEdit);
                return Ok(updated);
        }

        [HttpDelete("user/{userID}/card/{cardID}")]
        public ActionResult Delete(int userID, int cardID)
        {
                Card existing = cardDAO.GetCardByID(userID, cardID);
                if (existing == null)
                {
                    return NotFound("Card does not exist");
                }
                cardDAO.DeleteCard(userID, cardID);
                return NoContent();
        }
 
    }
}
