using BankAppBackend.Exceptions;
using BankAppBackend.Models;
using BankAppBackend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAppBackend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService customerService;
        private readonly IAccountService accountService;

        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            this.customerService = customerService;
            this.accountService = accountService;
        }

        [HttpPut("updateCustomer")]
        public ActionResult<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                return Ok(customerService.UpdateExistingCustomer(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // Logic to fetch data and return a response

        }

        [HttpGet("allCustomers")]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(customerService.GetAllCustomers());
        }

        [HttpGet("{customerId}")]
        public ActionResult<Customer> GetCustomerDetailsById(long customerId)
        {
            try
            {
                return Ok(customerService.FindCustomerById(customerId));
            }
            catch(Exception exception)
            {
                if(exception is EntityNotFound)
                {
                    return NotFound(exception.Message);
                }
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("customerAccounts/{customerId}")]
        public ActionResult<List<Account>> GetAccountsByCustomerId(long customerId)
        {
            return Ok(accountService.GetAccountsAgainstCustomerId(customerId));
        }

        [HttpGet("customerAccount/{customerId}")]
        public ActionResult<Account> GetAccountByCustomerId(long customerId)
        {
            return Ok(accountService.GetAccountAgainstCustomerId(customerId));
        }

    }
}

