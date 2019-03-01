using BlogWeb.Filters;
using BlogWeb.QueryService;
using BlogWeb.QueryService.Dtos.Param;
using IArticleApplication;
using IArticleApplication.Params;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleApplicationService _articleApplicationService;
        private readonly ArticleQueryService _articleQueryService;

        public ArticleController(IArticleApplicationService articleApplicationService, ArticleQueryService articleQueryService)
        {
            _articleApplicationService = articleApplicationService;
            _articleQueryService = articleQueryService;
        }

        public IActionResult Detail(Guid id)
        {
            return Json(_articleQueryService.FindArticleById(id));
        }

        [AdminRole]
        public IActionResult Publish(Guid id)
        {
            _articleApplicationService.PublishArticle(id);
            return Json(new {code = 0});
        }

        [AdminRole]
        public IActionResult Delete(Guid id)
        {
            _articleApplicationService.DeleteArticle(id);
            return Json(new {code = 0});
        }

        public IActionResult Index(QueryArticleParam param)
        {
            var pageInfo = _articleQueryService.QueryArticleByPage(param);
            pageInfo.List.ForEach(a =>
            {
                if (a.Content != null && a.Content.Length > 255) a.Content = a.Content.Substring(0, 255);
            });
            return Json(pageInfo);
        }

        [AdminRole]
        public IActionResult Create([FromBody] CreateArticleParam param)
        {
            _articleApplicationService.CreateArticle(param);
            return Json(new {code = 0});
        }
    }
}