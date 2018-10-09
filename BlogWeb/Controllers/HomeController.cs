using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Directory.GetCurrentDirectory() + "/wwwroot/index.html", "text/html");
        }
    }
}