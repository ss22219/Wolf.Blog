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
        private readonly IIntegrationEventBus _integrationEventBus;
        private readonly List<ArticleDetail> _articleDetails = new List<ArticleDetail>();
        private readonly IConfig _config;
        private readonly ArticleRepository _articleRepository;

        public ArticleQueryService(IIntegrationEventBus integrationEventBus, IConfig config,
            ArticleRepository articleRepository)
        {
            _integrationEventBus = integrationEventBus;
            _integrationEventBus.SubscribeEvent<NewArticleCreatedEvent>(Hanlde);
            _integrationEventBus.SubscribeEvent<ArticleUpdatedEvent>(Hanlde);
            _config = config;
            _articleRepository = articleRepository;

            _articleDetails = _articleRepository.GetAllEntity().Select(_articleRepositoryGetAllEntity =>
                new ArticleDetail()
                {
                    Id = _articleRepositoryGetAllEntity.Id,
                    Title = _articleRepositoryGetAllEntity.Title,
                    Content = _articleRepositoryGetAllEntity.Content,
                    CategoryId = _articleRepositoryGetAllEntity.CategoryId,
                    State = (ArticleDetailState) _articleRepositoryGetAllEntity.State,
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
            var pageInfo = new ArticlePageInfo() {List = new List<ArticleDetail>()};
            var pageSize = int.Parse(_config["PageSize"] ?? "20");
            var skip = (param.Page - 1) * pageSize;
            skip = Math.Max(0, skip);
            skip = (int) Math.Min(Math.Ceiling(_articleDetails.Count / (double) param.Page), skip);

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
                Id = ev.Data.Id,
                CategoryId = ev.Data.CategoryId,
                Content = ev.Data.Content,
                CreateDate = ev.Data.CreateDate,
                State = ev.Data.State,
                Tags = ev.Data.Tags,
                Title = ev.Data.Title
            });
        }

        private void Hanlde(ArticleUpdatedEvent ev)
        {
            var detail = _articleDetails.FirstOrDefault(a => a.Id == ev.NewValues.Id);
            if (detail == null)
                _articleDetails.Add(ev.NewValues);
            else
            {
                detail.Content = ev.NewValues.Content;
                detail.CategoryId = ev.NewValues.CategoryId;
                detail.Content = ev.NewValues.Content;
                detail.CreateDate = ev.NewValues.CreateDate;
                detail.State = ev.NewValues.State;
                detail.Tags = ev.NewValues.Tags;
                detail.Title = ev.NewValues.Title;
            }
        }
    }
}