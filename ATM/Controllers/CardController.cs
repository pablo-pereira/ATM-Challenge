using ATM.DAL;
using ATM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly IRepository<Cards> _cardDAL;

        public CardController(ILogger<CardController> logger, IRepository<Cards> cardDAL)
        {
            _logger = logger;
            _cardDAL = cardDAL;
        }

        
        [HttpGet("{cardNumber}", Name = "Get")]
        public ActionResult Get(string cardNumber)
        {
            var card = _cardDAL.Get(c => c.Number == cardNumber).FirstOrDefault();
            if (card != null && !card.IsLoked)
            {
                if (card.IsLoked)
                    return Conflict("Your card has been blocked.");
                HttpContext.Session.SetInt32("_CardId", card.CardId);
                return Ok();
            }
            return NoContent();
        }

        
        [HttpPut("{pin}", Name = "ValidatePin")]
        public ActionResult ValidatePin(int pin)
        {
            int cardId = HttpContext.Session.GetInt32("_CardId").HasValue ? HttpContext.Session.GetInt32("_CardId").Value : -1;
            if (cardId == -1)
                return NoContent();
            Cards card = _cardDAL.Get(c => c.CardId == cardId && c.Pin == pin).FirstOrDefault();
            if (card != null)
            {
                return new JsonResult(card);
            }
            else//incorrect pin, add erro
            {
                string errorMsg;
                card = _cardDAL.Get(c => c.CardId == cardId).FirstOrDefault();
                card.ErrorsCount += 1;
                errorMsg = "Wrong pin";
                if (card.ErrorsCount == 4)
                {
                    card.IsLoked = true;
                    errorMsg = "Your card has been blocked.";
                }
                _cardDAL.Update(card);
                _cardDAL.Save();
                return Conflict(errorMsg);
            }
        }
    }
}
