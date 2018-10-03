using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace ArticleRepository
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private List<ArticleCategoryEntity> articleEntities = new List<ArticleCategoryEntity>();

        public void Add(ArticleCategory articleCategory)
        {
            articleEntities.Add(new ArticleCategoryEntity() { ArticleCategory = articleCategory });
        }

        public string FindIdByName(string name)
        {
            return articleEntities.FirstOrDefault(a => a.ArticleCategory.Name == name)?.ArticleCategory.Id;
        }

        public ArticleCategory Restore(string id, out int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.ArticleCategory.Id == id);
            version = entity?.Version ?? 0;
            return entity?.ArticleCategory;
        }

        public bool Update(ArticleCategory articleCategory, int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.ArticleCategory.Id == articleCategory.Id);
            if (entity == null)
                articleEntities.Add(new ArticleCategoryEntity() { ArticleCategory = articleCategory, Version = ++version });
            if (entity.Version != version)
                return false;

            entity.ArticleCategory = articleCategory;
            entity.Version++;
            return true;
        }

        private class ArticleCategoryEntity
        {
            public ArticleCategory ArticleCategory { get; set; }
            public int Version { get; set; }
        }
    }
}
