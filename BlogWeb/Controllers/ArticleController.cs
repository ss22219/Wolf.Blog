using IArticleApplication;
using IArticleApplication.Params;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleApplicationService _articleApplicationService;

        public ArticleController(IArticleApplicationService articleApplicationService)
        {
            _articleApplicationService = articleApplicationService;
        }

        public IActionResult Detail(string id)
        {
            return Json(_articleApplicationService.FindArticleById(id));
        }

        public IActionResult Publish(string id)
        {
            _articleApplicationService.PublishArticle(id);
            return Json(new {code = 0});
        }

        public IActionResult Delete(string id)
        {
            _articleApplicationService.DeleteArticle(id);
            return Json(new {code = 0});
        }

        public IActionResult Index(QueryArticleParam param)
        {
            var pageInfo = _articleApplicationService.QueryArticleByPage(param);
            pageInfo.List.ForEach(a =>
            {
                if (a.Content != null && a.Content.Length > 255) a.Content = a.Content.Substring(0, 255);
            });
            return Json(pageInfo);
        }

        public IActionResult Create([FromBody] CreateArticleParam param)
        {
            _articleApplicationService.CreateArticle(param);
            return Json(new {code = 0});
        }
    }
}