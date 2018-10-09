using System.Collections.Generic;
using ArticleDomain.AggregateRoots;
using Zaaby.DDD.Abstractions.Domain;
using static ArticleDomain.AggregateRoots.Article;

namespace ArticleDomain.DomainEvents
{
    public class NewArticleCreateDomainEvent : IDomainEvent
    {
        public NewArticleCreateDomainEvent(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            CategoryId = article.CategoryId;
            Tags = article.Tags;
            State = article.State;
        }

        public string Id { get; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content { get; }

        /// <summary>
        ///     文章分类ID
        /// </summary>
        public string CategoryId { get; }

        /// <summary>
        ///     文章状态
        /// </summary>
        public ArticleState State { get; }

        /// <summary>
        ///     文章标签
        /// </summary>
        public IList<string> Tags { get; }
    }
}