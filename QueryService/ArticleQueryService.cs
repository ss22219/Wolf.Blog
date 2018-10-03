using ArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using Infrastracture.Configuration.Abstractions;
using MemoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

namespace QueryService
{
    public class ArticleQueryService : IArticleQueryService
    {
        private IIntegrationEventBus _integrationEventBus;
        private List<ArticleDetail> _articleDetails = new List<ArticleDetail>();
        private IConfig _config;
        private ArticleRepository _articleRepository;

        public ArticleQueryService(IIntegrationEventBus integrationEventBus, IConfig config, ArticleRepository articleRepository)
        {
            _integrationEventBus = integrationEventBus;
            _integrationEventBus.SubscribeEvent<NewArticleCreatedEvent>(Hanlde);
            _config = config;
            _articleRepository = articleRepository;

            _articleDetails = _articleRepository.GetAllEntity().Select(_articleRepositoryGetAllEntity => new ArticleDetail()
            {
                Id = _articleRepositoryGetAllEntity.Id,
                Title = _articleRepositoryGetAllEntity.Title,
                Content = _articleRepositoryGetAllEntity.Content,
                CategoryId = _articleRepositoryGetAllEntity.CategoryId,
                State = (ArticleDetailState)_articleRepositoryGetAllEntity.State,
                Tags = _articleRepositoryGetAllEntity.Tags,
                CreateDate = _articleRepositoryGetAllEntity.CreateDate
            }).ToList();
        }

        public ArticleDetail GetArticleDetail(string id)
        {
            return _articleDetails.FirstOrDefault(a => a.Id == id);
        }

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            var pageInfo = new ArticlePageInfo() { List = new List<ArticleDetail>() };
            var pageSize = int.Parse(_config["PageSize"] ?? "20");
            var skip = (param.Page - 1) * pageSize;
            skip = Math.Max(0, skip);
            skip = (int)Math.Min(Math.Ceiling(_articleDetails.Count / (double)param.Page), skip);

            var queryList = _articleDetails;
            queryList = queryList.Skip(skip).Take(pageSize + 1).ToList();
            if (queryList.Count > pageSize)
            {
                queryList.RemoveAt(pageSize - 1);
                pageInfo.NextPage = true;
            }
            pageInfo.List = queryList;
            return pageInfo;
        }

        private void Hanlde(NewArticleCreatedEvent ev)
        {
            _articleDetails.Add(new ArticleDetail()
            {
                Id = ev.Id,
                CategoryId = ev.CategoryId,
                Content = ev.Content,
                CreateDate = ev.CreateDate,
                State = (ArticleDetailState)ev.State,
                Tags = ev.Tags,
                Title = ev.Title
            });
        }
    }
}
