using ArticleDomain.AggregateRoots;
using BlogWeb.QueryService.Dtos.Res;
using IArticleApplication.IntegrationEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zaaby.DDD.Abstractions.Application;

namespace BlogWeb.QueryService
{
    public class CategoryQueryService : IIntegrationEventHandler<NewCreatedCategoryEvent>
    {
        private string _baseDir = Environment.GetEnvironmentVariable("DATA_DIR") ?? "./";
        private string SaveDir => _baseDir.TrimEnd(new char[] { '/' }) + "/category/";
        private string ListFile => $"{SaveDir}list.txt";
        private string GetSavePath(Guid id) => $"{SaveDir}{id}";

        public List<Category> AllCategory()
        {
            if (!File.Exists(ListFile))
                return new List<Category>();
            return File.ReadAllLines(ListFile).Where(i => !string.IsNullOrEmpty(i)).Select(id => FindById(Guid.Parse(id))).Where(c => c != null).Select(c => new Category { Id = c.Id, Name = c.Name, ArticleQuantity = c.ArticleQuantity }).ToList();
        }

        private ArticleCategory FindById(Guid id)
        {
            if (!File.Exists(GetSavePath(id)))
                return null;
            return JsonConvert.DeserializeObject<ArticleCategory>(File.ReadAllText(GetSavePath(id)));
        }

        public void Handle(NewCreatedCategoryEvent integrationEvent)
        {
            File.AppendAllText(ListFile, integrationEvent.Id.ToString() + "\r\n");
        }
    }
}
