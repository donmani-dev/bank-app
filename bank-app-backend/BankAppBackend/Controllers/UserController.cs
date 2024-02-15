using BankAppBackend.Models;
using BankAppBackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicantService applicantService;
        private readonly ITellerService tellerService;
        public UserController(IApplicantService applicantService, ITellerService tellerService)
        {
            this.tellerService = tellerService;
            this.applicantService = applicantService;
        }

        [HttpPost("login")]
        public ActionResult<object> LoginUser([FromBody] User user)
        {
            try
            {
                if (user.UserType.Equals(UserType.TELLER))
                {
                    Teller? teller = tellerService.GetTellerDetailsByLoginCredentials(user.Email, user.Password);
                    if (teller == null)
                    {
                        return Unauthorized("Login credentials are wrong");
                    }
                    return Ok(teller);
                }

                Applicant? applicant = applicantService.GetApplicantDetailsFromCredentials(user.Email, user.Password);
                if (applicant == null)
                {
                    return Unauthorized("Login credentials are wrong");
                }

                return Ok(applicant);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
