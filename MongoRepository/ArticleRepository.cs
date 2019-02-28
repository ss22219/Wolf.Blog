using System;
using System.Collections.Generic;
using System.Linq;
using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using Zaabee.Mongo.Abstractions;

namespace MongoRepository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IZaabeeMongoClient _client;

        public ArticleRepository(IZaabeeMongoClient client)
        {
            _client = client;
        }

        public Guid? FindIdByTitle(string title)
        {
            return _client.GetQueryable<ArticleEntity>().FirstOrDefault(a => a.Title == title)?.Id;
        }

        public void Add(Article article)
        {
            _client.Add(new ArticleEntity(article));
        }

        public bool Update(Article article, int version)
        {
            var entity = _client.GetQueryable<ArticleEntity>().FirstOrDefault(e => e.Id == article.Id);
            if (entity == null)
            {
                entity = new ArticleEntity(article) {Version = ++version};
                _client.Add(entity);
                return true;
            }

            if (entity.Version != version)
                return false;

            entity.Version++;
            _client.Update(entity);
            return true;
        }

        public Article Restore(Guid id, out int version)
        {
            var entity = _client.GetQueryable<ArticleEntity>().FirstOrDefault(a => a.Id == id);
            version = entity?.Version ?? 0;
            if (entity == null)
                return null;
            return new Article(entity.Id, entity.Title, entity.Content,
                entity.CreateDate, (Article.ArticleState) entity.State,
                entity.CategoryId, entity.Tags);
        }

        public List<ArticleEntity> GetAllEntity()
        {
            return _client.GetQueryable<ArticleEntity>().ToList();
        }
    }


    public class ArticleEntity
    {
        public ArticleEntity()
        {
        }

        public ArticleEntity(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            CategoryId = article.CategoryId;
            State = (int) article.State;
            Tags = article.Tags;
            CreateDate = article.CreateDate;
        }

        public Guid Id { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     文章分类ID
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        ///     文章状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     文章标签
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     版本
        /// </summary>
        public int Version { get; set; }

        public void Update(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            CategoryId = article.CategoryId;
            State = (int) article.State;
            Tags = article.Tags;
            CreateDate = article.CreateDate;
        }
    }
}