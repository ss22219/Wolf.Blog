using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using IArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using System;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using static ArticleDomain.AggregateRoots.Article;

namespace ArticleApplication
{
    /// <summary>
    /// 文章应用服务
    /// </summary>
    public class ArticleApplicationService : IArticleApplicationService
    {
        private ArticleDomainService _articleDomainService;
        private IArticleRepository _articleRepository;
        private IArticleQueryService _articleQueryService;
        private IIntegrationEventBus _integrationEventBus;

        public ArticleApplicationService(ArticleDomainService articleDomainService, IArticleRepository articleRepository, IArticleQueryService articleQueryService, IIntegrationEventBus integrationEventBus)
        {
            this._articleDomainService = articleDomainService;
            this._articleRepository = articleRepository;
            this._articleQueryService = articleQueryService;
            this._integrationEventBus = integrationEventBus;
        }

        public void CreateArticle(CreateArticleParam param)
        {
            var article = new Article(param.Id, param.Title, param.Content, DateTime.Now, (ArticleState)param.State, param.CategoryId, param.Tags);
            _articleDomainService.CreateArticle(article);
            _integrationEventBus.PublishEvent(new NewArticleCreatedEvent(article.Id, article.Title, article.Content, article.CreateDate, (NewArticleCreatedState)article.State, article.CategoryId, article.Tags));
        }

        public ArticleDetail FindArticleById(string id)
        {
            return _articleQueryService.GetArticleDetail(id);
        }

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            return _articleQueryService.QueryArticleByPage(param);
        }
    }
}
