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
                    State = IArticleApplication.Params.CreateArticleState.Draft,
                    Title = "test"
                });

            return Json(pageInfo);
        }
    }
}
