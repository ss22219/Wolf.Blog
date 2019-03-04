using BlogWeb.QueryService.Dtos.Param;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
using Infrastracture.Configuration.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zaaby.DDD.Abstractions.Application;

namespace BlogWeb.QueryService
{
    public class ArticleQueryService : IIntegrationEventHandler<NewArticleCreatedEvent>
    {
        private string _baseDir = Environment.GetEnvironmentVariable("DATA_DIR") ?? "./";
        private string SaveDir => _baseDir.TrimEnd(new char[] { '/' }) + "/article/";
        private string ListFile => $"{SaveDir}list.txt";
        private int pageSize;

        public ArticleQueryService()
        {
            pageSize = 10;
        }

        public ArticleQueryService(IConfig config)
        {
            pageSize = config.Get<int>("PageSize");
        }

        private string GetSavePath(Guid id) => $"{SaveDir}{id}";

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            if (!File.Exists(ListFile))
                return new ArticlePageInfo { NextPage = false, List = new List<ArticleDetail>() };
            var list = File.ReadAllLines(ListFile).Where(i => !string.IsNullOrEmpty(i)).ToArray();
            if (list.Length < pageSize * param.Page)
                return new ArticlePageInfo { NextPage = false, List = new List<ArticleDetail>() };

            return new ArticlePageInfo
            {
                List = list.Skip(pageSize * param.Page).Take(pageSize).Select(id => FindArticleById(Guid.Parse(id))).Where(a => a != null).ToList(),
                NextPage = list.Length < (pageSize * param.Page + 1)
            };
        }

        public ArticleDetail FindArticleById(Guid id)
        {
            if (!File.Exists(GetSavePath(id)))
                return null;
            return JsonConvert.DeserializeObject<ArticleDetail>(File.ReadAllText(GetSavePath(id)));
        }

        public void Handle(NewArticleCreatedEvent integrationEvent)
        {
            File.AppendAllText(ListFile, integrationEvent.Data.Id.ToString() + "\r\n");
        }
    }
}