using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankTrackingSystem.Models;
using BankTrackingSystem.Data;
using Microsoft.Extensions.Configuration;

namespace BankTrackingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApplicantMessagesRespository _applicantMessagesRespository;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IApplicantMessagesRespository applicantMessagesRespository, IConfiguration configuration)
    {
        _logger = logger;
        _applicantMessagesRespository = applicantMessagesRespository;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Details(string emailAddress)
    {
        ConnectionStringsOptions connectionStringsOptions = new ConnectionStringsOptions();
        _configuration.GetSection(ConnectionStringsOptions.ConnectionStrings).Bind(connectionStringsOptions);
        var messages = await _applicantMessagesRespository.GetAllAgainstEmailAddress(emailAddress);
        ViewData["messages"] = messages;
        ViewData["connection"] = new System.Uri(connectionStringsOptions.FrontEndConnectionString);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
