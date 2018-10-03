using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogWeb.Models;
using IArticleApplication;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private IArticleApplicationService _articleApplicationService;
        public HomeController(IArticleApplicationService articleApplicationService)
        {
            _articleApplicationService = articleApplicationService;
        }

        public IActionResult Index()
        {
            var pageInfo = _articleApplicationService.QueryArticleByPage(new IArticleApplication.Params.QueryArticleParam());
            if (pageInfo.List.Count == 0)
                _articleApplicationService.CreateArticle(new IArticleApplication.Params.CreateArticleParam()
                {
                    Content = "test",
                    Id = Guid.NewGuid().ToString(),
                    State = IArticleApplication.Params.CreateArticleState.Draft,
                    Title = "test"
                });

            return Json(pageInfo);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
}
