using System.Collections.Generic;
using System.Linq;
using ArticleApplication;
using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using IArticleApplication.Model;
using Zaabee.Mongo.Abstractions;

namespace MongoRepository
{
    public class ArticleCategoryRepository : IArticleCategoryRepository, ICategoryQueryService
    {
        private readonly IZaabeeMongoClient _client;

        public ArticleCategoryRepository(IZaabeeMongoClient client)
        {
            _client = client;
        }

        public void Add(ArticleCategory article)
        {
            _client.Add(new ArticleCategoryEntity(article));
        }

        public string FindIdByName(string name)
        {
            return _client.GetQueryable<ArticleCategoryEntity>().FirstOrDefault(a => a.Name == name)?.Id;
        }

        public bool Update(ArticleCategory category, int version)
        {
            var entity = _client.GetQueryable<ArticleCategoryEntity>().FirstOrDefault(e => e.Id == category.Id);
            if (entity == null)
            {
                entity = new ArticleCategoryEntity(category) {Version = ++version};
                _client.Add(entity);
                return true;
            }

            if (entity.Version != version)
                return false;

            entity.Version++;
            _client.Update(entity);
            return true;
        }

        public ArticleCategory Restore(string id, out int version)
        {
            var entity = _client.GetQueryable<ArticleCategoryEntity>().FirstOrDefault(a => a.Id == id);
            version = entity?.Version ?? 0;
            if (entity == null)
                return null;
            return new ArticleCategory(entity.Id, entity.Name, entity.ArticleQuantity);
        }

        public void Delete(string id)
        {
            _client.Delete<ArticleCategoryEntity>(a => a.Id == id);
        }

        public IList<CategoryInfo> AllCategory()
        {
            return GetAllEntity().Select(e => new CategoryInfo
                {ArticalQuantity = e.ArticleQuantity, Name = e.Name, Id = e.Id}).ToList();
        }

        public List<ArticleCategoryEntity> GetAllEntity()
        {
            return _client.GetQueryable<ArticleCategoryEntity>().ToList();
        }
    }

    public class ArticleCategoryEntity
    {
        public ArticleCategoryEntity()
        {
        }

        public ArticleCategoryEntity(ArticleCategory articleCategory)
        {
            Id = articleCategory.Id;
            Name = articleCategory.Name;
            ArticleQuantity = articleCategory.ArticleQuantity;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int ArticleQuantity { get; set; }
        public int Version { get; set; }

        public void Update(ArticleCategory articleCategory)
        {
            Name = articleCategory.Name;
            ArticleQuantity = articleCategory.ArticleQuantity;
        }
    }
}