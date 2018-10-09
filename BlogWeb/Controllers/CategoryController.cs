using IArticleApplication;
using IArticleApplication.Params;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IArticleApplicationService _articleApplicationService;

        public CategoryController(IArticleApplicationService articleApplicationService)
        {
            _articleApplicationService = articleApplicationService;
        }

        public IActionResult Index()
        {
            return Json(_articleApplicationService.AllCategory());
        }

        public IActionResult Delete(string id)
        {
            _articleApplicationService.DeleteCategory(id);
            return Json(new {code = 0});
        }

        public IActionResult Create([FromBody] CreateCategoryParam param)
        {
            _articleApplicationService.CreateCategory(param);
            return Json(new {code = 0});
        }
    }
}