using Microsoft.AspNetCore.Mvc;
using NumberSorter.WebUI.Models;
using System.Diagnostics;

namespace NumberSorter.WebUI.Controllers;

public class HomeController() : Controller
{
    public IActionResult Index() => this.View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
}
