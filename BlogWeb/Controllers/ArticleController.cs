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
            _articleApplicationService.Publish(id);
            return Json(new {code = 0});
        }
        
        public IActionResult Delete(string id)
        {
            _articleApplicationService.Delete(id);
            return Json(new {code = 0});
        }

        public IActionResult Create(CreateArticleParam param)
        {
            _articleApplicationService.CreateArticle(param);
            return Json(new {code = 0});
        }
    }
}