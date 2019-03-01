using BlogWeb.Filters;
using BlogWeb.QueryService;
using IArticleApplication;
using IArticleApplication.Params;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryQueryService _categoryQueryService;
        private readonly IArticleApplicationService _articleApplicationService;

        public CategoryController(IArticleApplicationService articleApplicationService, CategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
            _articleApplicationService = articleApplicationService;
        }

        public IActionResult Index()
        {
            return Json(_categoryQueryService.AllCategory());
        }

        [AdminRole]
        public IActionResult Delete(Guid id)
        {
            _articleApplicationService.DeleteCategory(id);
            return Json(new { code = 0 });
        }

        [AdminRole]
        public IActionResult Create([FromBody] CreateCategoryParam param)
        {
            _articleApplicationService.CreateCategory(param);
            return Json(new { code = 0 });
        }
    }
}