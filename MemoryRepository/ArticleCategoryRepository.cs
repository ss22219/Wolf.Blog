using System.Collections.Generic;
using System.Linq;
using ArticleApplication;
using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using IArticleApplication.Model;

namespace MemoryRepository
{
    public class ArticleCategoryRepository : IArticleCategoryRepository, ICategoryQueryService
    {
        private readonly List<ArticleCategoryEntity> entities = new List<ArticleCategoryEntity>();

        public void Add(ArticleCategory articleCategory)
        {
            entities.Add(new ArticleCategoryEntity {ArticleCategory = articleCategory});
        }

        public string FindIdByName(string name)
        {
            return entities.FirstOrDefault(a => a.ArticleCategory.Name == name)?.ArticleCategory.Id;
        }

        public ArticleCategory Restore(string id, out int version)
        {
            var entity = entities.FirstOrDefault(e => e.ArticleCategory.Id == id);
            version = entity?.Version ?? 0;
            return entity?.ArticleCategory;
        }

        public void Delete(string id)
        {
            var category = entities.FirstOrDefault(a => a.ArticleCategory.Id == id);
            if (category != null)
                entities.Remove(category);
        }

        public bool Update(ArticleCategory articleCategory, int version)
        {
            var entity = entities.FirstOrDefault(e => e.ArticleCategory.Id == articleCategory.Id);
            if (entity == null)
            {
                entities.Add(
                    new ArticleCategoryEntity {ArticleCategory = articleCategory, Version = ++version});
                return true;
            }

            if (entity.Version != version)
                return false;

            entity.ArticleCategory = articleCategory;
            entity.Version++;
            return true;
        }

        public IList<CategoryInfo> AllCategory()
        {
            return entities.Select(e => new CategoryInfo
            {
                ArticalQuantity = e.ArticleCategory.ArticleQuantity,
                Name = e.ArticleCategory.Name,
                Id = e.ArticleCategory.Id
            }).ToList();
        }

        private class ArticleCategoryEntity
        {
            public ArticleCategory ArticleCategory { get; set; }
            public int Version { get; set; }
        }
    }
}