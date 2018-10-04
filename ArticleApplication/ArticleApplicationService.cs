using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using IArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using System;
using System.Collections.Generic;
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
        private readonly ArticleCategoryDomainService _articleCategoryDomainService;
        private readonly IArticleQueryService _articleQueryService;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly IIntegrationEventBus _integrationEventBus;

        public ArticleApplicationService(ArticleDomainService articleDomainService,
            IArticleQueryService articleQueryService,
            IIntegrationEventBus integrationEventBus,
            ArticleCategoryDomainService articleCategoryDomainService,
            ICategoryQueryService categoryQueryService)
        {
            _articleDomainService = articleDomainService;
            _articleQueryService = articleQueryService;
            _integrationEventBus = integrationEventBus;
            _articleCategoryDomainService = articleCategoryDomainService;
            _categoryQueryService = categoryQueryService;
        }

        public void CreateArticle(CreateArticleParam param)
        {
            var article = new Article(GuidHelper.GenerateComb().ToString(), param.Title, param.Content, DateTime.Now,
                (ArticleState) param.State,
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

        public void PublishArticle(string id)
        {
            var article = _articleDomainService.PublishArticle(id, out int version);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent()
            {
                Data = new ArticleEventData()
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags,
                    Version = version
                }
            });
        }

        public void DeleteArticle(string id)
        {
            var article = _articleDomainService.DeleteArticle(id, out int version);
            _integrationEventBus.PublishEvent(new ArticleUpdatedEvent()
            {
                Data = new ArticleEventData()
                {
                    Id = article.Id, Title = article.Title, Content = article.Content, CreateDate = article.CreateDate,
                    State = (ArticleDetailState) article.State, CategoryId = article.CategoryId, Tags = article.Tags,
                    Version = version
                }
            });
        }

        public void DeleteCategory(string id)
        {
            _articleCategoryDomainService.Delete(id);
            _integrationEventBus.PublishEvent(new DeletedCategoryEvent()
            {
                Id = id
            });
        }

        public IList<CategoryInfo> AllCategory()
        {
            return _categoryQueryService.AllCategory();
        }

        public void CreateCategory(CreateCategoryParam param)
        {
            var category = new ArticleCategory(GuidHelper.GenerateComb().ToString(), param.Name, 0);
            _articleCategoryDomainService.Create(category);
            _integrationEventBus.PublishEvent(new NewCreatedCategoryEvent()
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
    }
}