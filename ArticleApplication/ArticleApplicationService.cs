using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainServices;
using IArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using Infrastracture.Utilities;
using System;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using static ArticleDomain.AggregateRoots.Article;

namespace ArticleApplication
{
    /// <summary>
    ///     文章应用服务
    /// </summary>
    public class ArticleApplicationService : IArticleApplicationService
    {
        private readonly ArticleCategoryDomainService _articleCategoryDomainService;
        private readonly ArticleDomainService _articleDomainService;
        private readonly IIntegrationEventBus _integrationEventBus;

        public ArticleApplicationService(ArticleDomainService articleDomainService,
            IIntegrationEventBus integrationEventBus,
            ArticleCategoryDomainService articleCategoryDomainService)
        {
            _articleDomainService = articleDomainService;
            _integrationEventBus = integrationEventBus;
            _articleCategoryDomainService = articleCategoryDomainService;
        }

        public void CreateArticle(CreateArticleParam param)
        {
            var article = new Article(GuidHelper.GenerateComb(), param.Title, param.Content, DateTime.Now,
                (ArticleState) param.State,
                param.CategoryId, param.Tags);
            _articleDomainService.CreateArticle(article);
            _integrationEventBus.PublishEvent(new NewArticleCreatedEvent(article.Id, article.Title, article.Content,
                article.CreateDate, (ArticleDetailState) article.State, article.CategoryId, article.Tags));
        }

        public void PublishArticle(Guid id)
        {
            var article = _articleDomainService.PublishArticle(id, out var version);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent
            {
                Data = new ArticleEventData
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags,
                    Version = version
                }
            });
        }

        public void DeleteArticle(Guid id)
        {
            var article = _articleDomainService.DeleteArticle(id, out var version);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent
            {
                Data = new ArticleEventData
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags,
                    Version = version
                }
            });
        }

        public void DeleteCategory(Guid id)
        {
            _articleCategoryDomainService.Delete(id);
            _integrationEventBus.PublishEvent(new DeletedCategoryEvent
            {
                Id = id
            });
        }

        public void CreateCategory(CreateCategoryParam param)
        {
            var category = new ArticleCategory(GuidHelper.GenerateComb(), param.Name, 0);
            _articleCategoryDomainService.Create(category);
            _integrationEventBus.PublishEvent(new NewCreatedCategoryEvent
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}