using ATM.DAL;
using ATM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ILogger<OperationController> _logger;
        private readonly IRepository<Cards> _cardDAL;
        private readonly IRepository<Operations> _operationAL;


        public OperationController(ILogger<OperationController> logger, IRepository<Cards> cardDAL, IRepository<Operations> operationAL)
        {
            _logger = logger;
            _cardDAL = cardDAL;
            _operationAL = operationAL;
        }
    
       

        //Return operations by cardID
        // GET api/<OperationController>/5
        [HttpGet()]
        public ActionResult Get()
        {
            int cardId = HttpContext.Session.GetInt32("_CardId").HasValue ? HttpContext.Session.GetInt32("_CardId").Value : -1;
            if (cardId == -1)
                return NoContent();
            Cards card = _cardDAL.Get(c => c.CardId == cardId).FirstOrDefault();
            if (card != null)
            {
                var operations = _operationAL.Get(o => o.CardId == card.CardId);
                return new JsonResult(operations);
            }
            else
                return NoContent();
        }

        // POST api/<OperationController>
        [HttpPost]
        public ActionResult Post([FromBody] Operations operation)
        {
            try
            {
                int cardId = HttpContext.Session.GetInt32("_CardId").HasValue ? HttpContext.Session.GetInt32("_CardId").Value : -1;
                if (cardId != -1 || operation.CardId == cardId)
                {
                    Cards card = _cardDAL.Get(c => c.CardId == cardId).FirstOrDefault();
                    operation.DateOperation = DateTime.Now;
                    if (operation.OperationType == OperationType.Withdrawal && operation.Amount.Value < card.Balance)
                    {
                        _operationAL.Add(operation);
                        _operationAL.Save();

                        card.Balance = card.Balance - operation.Amount.Value;
                        _cardDAL.Update(card);
                        _cardDAL.Save();
                    }
                    else
                        return Conflict("Insufficient balance");
                    if (operation.OperationType != OperationType.Withdrawal)
                    {
                        _operationAL.Add(operation);
                        _operationAL.Save();
                    }
                    return new JsonResult(operation);
                }
                return Unauthorized();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }           
        }
    }
}
