using BlogWeb.QueryService.Dtos.Param;
using IArticleApplication.IntegrationEvents;
using IArticleApplication.Model;
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
        public string _baseDir = Environment.GetEnvironmentVariable("DATA_DIR") ?? "./";
        public string SaveDir => _baseDir.TrimEnd(new char[] { '/' }) + "/article/";
        public string ListFile => $"{SaveDir}list.txt";

        private string GetSavePath(Guid id) => $"{SaveDir}{id}";

        public ArticlePageInfo QueryArticleByPage(QueryArticleParam param)
        {
            if (!File.Exists(ListFile))
                return new ArticlePageInfo { NextPage = false, List = new List<ArticleDetail>() };
            var list = File.ReadAllLines(ListFile).Where(i => !string.IsNullOrEmpty(i)).ToArray();
            var pageSize = 20;
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