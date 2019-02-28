using System;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticleDomain.AggregateRoots
{
    /// <summary>
    ///     文章分类
    /// </summary>
    public class ArticleCategory : IAggregateRoot<Guid>
    {
        public ArticleCategory(Guid id, string name, int articleQuantity)
        {
            Assert.IsNotNullOrWhiteSpace("文章分类名称", name);
            Id = id;
            Name = name;
            ArticleQuantity = articleQuantity;
        }

        public string Name { get; }
        public int ArticleQuantity { get; private set; }
        public Guid Id { get; }

        public void IncremntArticleQuantity()
        {
            ArticleQuantity++;
        }
    }
}