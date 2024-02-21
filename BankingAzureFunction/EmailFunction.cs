using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Azure.Communication.Email;
using System.Configuration;

namespace BankingAzureFunction
{
    public class EmailFunction
    {
        private readonly IConfiguration config;
        public EmailFunction(IConfiguration config)
        {
            this.config = config;
        }


        //const string ConnectionString = "endpoint=https://bankingappcommunicationservice.unitedstates.communication.azure.com/;accesskey=yGgzj0R9vJrB7C3rrwY/EcX2vLIEXA89G3rI0ra4xMDNvhsqNDkEjd6byNTBnIWAX5ThqAMJIeg08V9StCwctg==";
        [FunctionName("EmailFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            try
            {
                // Reading connection string
                string connectionString = Environment.GetEnvironmentVariable("EmailServiceConnectionString");;

                // Deserialize the request body into ApplicantMessageModel object
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ApplicantMessageModel applicantMessageData = JsonConvert.DeserializeObject<ApplicantMessageModel>(requestBody);

                // Initialize EmailClient with the connection string
                EmailClient emailClient = new EmailClient(connectionString);

                // Define email content
                string subject = "Bank Application Status Updated";
                string htmlContent = GetEmailContent(applicantMessageData);
                string sender = Environment.GetEnvironmentVariable("Email"); ;
                string recipient = applicantMessageData.ApplicantEmailAddress;

                // Send email
                await emailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    sender,
                    recipient,
                    subject,
                    htmlContent);

                string responseMessage = "Email sent successfully.";
                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred while processing the request."+ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        private string GetEmailContent(ApplicantMessageModel applicantMessageData)
        {
            string statusMessage = applicantMessageData.accountStatus == AccountStatus.APPROVED ?
                "Please register yourself at this URL :<a href='http://localhost.com'>Registeration Link</a>" : "";
            return $@"
                <html>
                <h4>{applicantMessageData.Message}</h4>
                <p>{statusMessage}</p>
                <p>Regards,<br>ABC Bank.</p>
                </html>";
        }
    }
}
