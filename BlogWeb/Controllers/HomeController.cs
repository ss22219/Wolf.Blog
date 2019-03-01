using System.IO;
using BlogWeb.Filters;
using Infrastracture.Configuration.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Directory.GetCurrentDirectory() + "/wwwroot/index.html", "text/html");
        }
        [AdminRole]
        public IActionResult Login()
        {
            return Redirect("~/");
        }
        
        public IActionResult IsLogin()
        {
            return Json(new { login = Request.Headers.ContainsKey("Authorization") });
        }
    }
}