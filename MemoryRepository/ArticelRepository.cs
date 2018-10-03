using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryRepository
{
    public class ArticleRepository : IArticleRepository
    {
        private List<ArticleEntity> articleEntities = new List<ArticleEntity>();

        public void Add(Article article)
        {
            articleEntities.Add(new ArticleEntity() { Article = article });
        }

        public string FindIdByTitle(string title)
        {
            return articleEntities.FirstOrDefault(a => a.Article.Title == title)?.Article.Id;
        }

        public Article Restore(string id, out int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.Article.Id == id);
            version = entity?.Version ?? 0;
            return entity?.Article;
        }

        public bool Update(Article article, int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.Article.Id == article.Id);
            if (entity == null)
                articleEntities.Add(new ArticleEntity() { Article = article, Version = ++version });
            if (entity.Version != version)
                return false;

            entity.Article = article;
            entity.Version++;
            return true;
        }

        private class ArticleEntity
        {
            public Article Article { get; set; }
            public int Version { get; set; }
        }
    }
}
