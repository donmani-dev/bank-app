using BankAppBackend.Exceptions;
using BankAppBackend.Models;
using BankAppBackend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BankAppBackend.Controllers
{
    [Route("api/teller")]
    [ApiController]
    public class TellerController : ControllerBase
    {
        private ITellerService tellerSevice;
        public TellerController(ITellerService applicantService)
        {
            tellerSevice = applicantService;
        }

        [HttpPut("changeStatus/{applicantId}")]
        public ActionResult ChangeApplicantStatus(long applicantId, [FromBody] ApplicantStatus accountStatus)
        {
            try
            {
                tellerSevice.ChangeApplicantStatus(applicantId, accountStatus.AccountStatus, accountStatus.TellerId);
                return Ok();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                if (exception is EntityNotFound)
                {
                    return NotFound(exception.Message);
                }
                else if (exception is EntityNotFound)
                {
                    return Conflict(exception.Message);
                }
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Teller> GetTellerById(long id)
        {
            try
            {
                return tellerSevice.GetTellerById(id);
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
        [HttpPost]

        public ActionResult<Teller> RegisterTeller(Teller teller)
        {
            try
            {
                return tellerSevice.RegisterTeller(teller);
            }
            catch(Exception exception)
            {
                if(exception is EntityAlreadyExist)
                {
                    return Conflict(exception.Message);
                }
                return BadRequest(exception.Message);
            } 
        }

        

    }
}
