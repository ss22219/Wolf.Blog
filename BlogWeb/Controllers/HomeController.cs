using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogWeb.Models;
using IArticleApplication;
using IArticleApplication.Params;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleApplicationService _articleApplicationService;

        public HomeController(IArticleApplicationService articleApplicationService)
        {
            _articleApplicationService = articleApplicationService;
        }

        public IActionResult Index(QueryArticleParam param)
        {
            var pageInfo = _articleApplicationService.QueryArticleByPage(param);
            if (pageInfo.List.Count == 0)
            {
                Task.Run(() => _articleApplicationService.CreateArticle(
                    new CreateArticleParam()
                    {
                        Content = "test",
                        State = CreateArticleState.Draft,
                        Title = "test"
                    }));
            }
            return Json(pageInfo);
        }
    }
}