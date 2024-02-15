using BankAppBackend.Exceptions;
using BankAppBackend.Models;
using BankAppBackend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAppBackend.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService) {
            this._transactionService = transactionService;
        }

        [HttpPost]
        public ActionResult<Applicant> AddTransaction([FromBody] TransactionExtended transaction)
        {
            try
            {
                return Ok(this._transactionService.AddTransaction(transaction));
            }
            catch(Exception exception)
            {
                Console.Write(exception.ToString());
                if(exception is EntityNotFound)
                {
                    return NotFound(exception.Message);
                }
                return BadRequest(exception.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<Transaction> GetTransactionById(Guid id)
        {
            try
            {
                return Ok(this._transactionService.GetTransactionById(id));
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.ToString());
                if(exception is EntityNotFound)
                {
                    return NotFound(exception.Message);
                }

                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Transaction>> GetAllTranscation()
        {
            IEnumerable<Transaction> txnList = this._transactionService.GetTransactions();
            return Ok(txnList);
        }

        [HttpGet("accountTransactions/{accountId}")]
        public ActionResult<List<Transaction>> GetAllTranscationByAccountId(Guid accountId)
        {
            IEnumerable<Transaction> txnList = this._transactionService.GetTransactionsByAccountId(accountId);
            return Ok(txnList);
        }
    }
}
