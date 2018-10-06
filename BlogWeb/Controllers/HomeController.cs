using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogWeb.Models;
using IArticleApplication;
using IArticleApplication.Params;
using System.IO;

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