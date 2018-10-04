using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using IArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using System;
using Infrastracture.Utilities;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using static ArticleDomain.AggregateRoots.Article;

namespace ArticleApplication
{
    /// <summary>
    /// 文章应用服务
    /// </summary>
    public class ArticleApplicationService : IArticleApplicationService
    {
        private readonly ArticleDomainService _articleDomainService;
        private readonly IArticleQueryService _articleQueryService;
        private readonly IIntegrationEventBus _integrationEventBus;

        public ArticleApplicationService(ArticleDomainService articleDomainService, IArticleQueryService articleQueryService,
            IIntegrationEventBus integrationEventBus)
        {
            _articleDomainService = articleDomainService;
            _articleQueryService = articleQueryService;
            _integrationEventBus = integrationEventBus;
        }

        public void CreateArticle(CreateArticleParam param)
        {
            var article = new Article(GuidHelper.GenerateComb().ToString(), param.Title, param.Content, DateTime.Now, (ArticleState) param.State,
                param.CategoryId, param.Tags);
            _articleDomainService.CreateArticle(article);
            _integrationEventBus.PublishEvent(new NewArticleCreatedEvent(article.Id, article.Title, article.Content,
                article.CreateDate, (ArticleDetailState) article.State, article.CategoryId, article.Tags));
        }

        public ArticleDetail FindArticleById(string id)
        {
            return _articleQueryService.GetArticleDetail(id);
        }

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            return _articleQueryService.QueryArticleByPage(param);
        }

        public void Publish(string id)
        {
            var article = _articleDomainService.PublishArticle(id);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent()
            {
                OldValues = _articleQueryService.GetArticleDetail(id),
                NewValues = new ArticleDetail()
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags
                }
            });
        }

        public void Delete(string id)
        {
            var article = _articleDomainService.DeleteArticle(id);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent()
            {
                OldValues = _articleQueryService.GetArticleDetail(id),
                NewValues = new ArticleDetail()
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags
                }
            });
        }
    }
}