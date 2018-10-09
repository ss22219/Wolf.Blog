using System;
using System.Collections.Generic;
using System.Linq;
using ArticleApplication;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using IArticleApplication.Params;
using Infrastracture.Configuration.Abstractions;
using MongoRepository;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

namespace QueryService
{
    public class ArticleQueryService : IArticleQueryService
    {
        private readonly List<ArticleEventData> _articleDetails = new List<ArticleEventData>();
        private readonly ArticleRepository _articleRepository;
        private readonly IConfig _config;
        private readonly IIntegrationEventBus _integrationEventBus;

        public ArticleQueryService(IIntegrationEventBus integrationEventBus, IConfig config,
            ArticleRepository articleRepository)
        {
            _integrationEventBus = integrationEventBus;
            _integrationEventBus.SubscribeEvent<NewArticleCreatedEvent>(Hanlde);
            _integrationEventBus.SubscribeEvent<ArticleUpdatedEvent>(Hanlde);
            _config = config;
            _articleRepository = articleRepository;

            _articleDetails = _articleRepository.GetAllEntity().Select(entity =>
                new ArticleEventData
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Content = entity.Content,
                    CategoryId = entity.CategoryId,
                    State = (ArticleDetailState) entity.State,
                    Tags = entity.Tags,
                    CreateDate = entity.CreateDate
                }).ToList();
        }

        public ArticleDetail GetArticleDetail(string id)
        {
            var data = _articleDetails.FirstOrDefault(a => a.Id == id);
            if (data == null)
                return null;
            return new ArticleDetail
            {
                Id = data.Id,
                Title = data.Title,
                Content = data.Content,
                CategoryId = data.CategoryId,
                State = data.State,
                Tags = data.Tags,
                CreateDate = data.CreateDate
            };
        }

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            var pageInfo = new ArticlePageInfo {List = new List<ArticleDetail>()};
            var pageSize = int.Parse(_config["PageSize"] ?? "20");
            var skip = (param.Page - 1) * pageSize;
            skip = Math.Max(0, skip);
            skip = (int) Math.Min(Math.Ceiling(_articleDetails.Count / (double) param.Page), skip);

            var queryList = _articleDetails.Where(a => a.State != ArticleDetailState.Deleted).ToList();
            var count = queryList.Count();
            if (count < (param.Page - 1) * pageSize)
                return pageInfo;
            queryList = queryList.Skip(skip).Take(pageSize + 1).ToList();
            if (queryList.Count > pageSize)
            {
                queryList.RemoveAt(pageSize - 1);
                pageInfo.NextPage = true;
            }

            pageInfo.List = queryList.Select(data => new ArticleDetail
            {
                Id = data.Id,
                Title = data.Title,
                Content = data.Content,
                CategoryId = data.CategoryId,
                State = data.State,
                Tags = data.Tags,
                CreateDate = data.CreateDate
            }).ToList();
            return pageInfo;
        }

        private void Hanlde(NewArticleCreatedEvent ev)
        {
            _articleDetails.Add(new ArticleEventData
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
            var detail = _articleDetails.FirstOrDefault(a => a.Id == ev.Data.Id);
            if (detail == null)
            {
                _articleDetails.Add(ev.Data);
            }
            else
            {
                if (detail.Version < ev.Data.Version)
                {
                    detail.Content = ev.Data.Content;
                    detail.CategoryId = ev.Data.CategoryId;
                    detail.Content = ev.Data.Content;
                    detail.CreateDate = ev.Data.CreateDate;
                    detail.State = ev.Data.State;
                    detail.Tags = ev.Data.Tags;
                    detail.Title = ev.Data.Title;
                    detail.Version = ev.Data.Version;
                }
            }
        }
    }
}