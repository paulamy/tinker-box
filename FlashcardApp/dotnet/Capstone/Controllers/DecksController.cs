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
    public class DecksController : ControllerBase
    {
        private readonly IDeckDao deckDAO;

        public DecksController(IDeckDao _deckDao)
        {
            deckDAO = _deckDao;
        }
        [HttpGet("{name}")]
        public ActionResult<Deck> GetDeckByName(string name)
        {
            Deck deck = deckDAO.GetDeckByName(name);
            return deck;
        }

        [HttpGet]
        public ActionResult<List<Deck>> GetPublicDecks()
        {
            List<Deck> decks = deckDAO.GetPublicDecks();
            return decks;
        }

        [HttpGet("user/{userID}")]
        public ActionResult<List<Deck>> GetUserDecks(int userID)
        {
            List<Deck> decks = deckDAO.GetUserDecks(userID);
            return decks;
        }

        [HttpGet("user/{userID}/deck/{deckID}")]
        public ActionResult<Deck> GetDeckByID(int userID, int deckID)
        {

            Deck deck = deckDAO.GetDeckByID(userID, deckID);
            if (deck != null)
            {
                return Ok(deck);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("user/{userID}")]
        public ActionResult<Deck> AddDeck(int userID, Deck newDeck)
        {

            try
            {
                Deck created = deckDAO.AddDeck(userID, newDeck);
                return Created($"[controller]/createCard/{userID}/{created.DeckID}", created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPut("user/{userID}/deck/{deckID}")]
        public ActionResult<Deck> ModifyDeck(int userID, int deckID, Deck deckToEdit)
        {

            Deck pending = deckDAO.GetDeckByID(userID, deckID);
            if (pending == null)
            {
                return NotFound("Deck does not exist");
            }
            else if (deckID != pending.DeckID)
            {
                return NotFound("Deck does not exist");
            }
            Deck updated = deckDAO.ModifyDeck(userID, deckID, deckToEdit);
            return Ok(updated);

        }

        [HttpDelete("user/{userID}/deck/{deckID}")]
        public ActionResult Delete(int userID, int deckID)
        {

            Deck existing = deckDAO.GetDeckByID(userID, deckID);
            if (existing == null)
            {
                return NotFound("Deck does not exist");
            }
            deckDAO.DeleteDeck(userID, deckID);
            return NoContent();

        }

    }
}
