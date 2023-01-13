using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeakySecrets.Models;

namespace LeakySecrets.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly IConfiguration _config;

  public HomeController(ILogger<HomeController> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }

  public IActionResult Index()
  {
    var secret = _config.GetValue<string>("Secret");
    ViewData["Secret"] = secret ?? "No Secret Found";
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
