using ArticleDomain.AggregateRoots;
using System.Collections.Generic;
using Zaaby.DDD.Abstractions.Domain;
using static ArticleDomain.AggregateRoots.Article;

namespace ArticleDomain.DomainEvents
{
    public class NewArticleCreateDomainEvent : IDomainEvent
    {
        public string Id { get; private set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 文章分类ID
        /// </summary>
        public string CategoryId { get; private set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public ArticleState State { get; private set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IList<string> Tags { get; private set; }

        public NewArticleCreateDomainEvent(Article article)
        {
            this.Id = article.Id;
            this.Title = article.Title;
            this.Content = article.Content;
            this.CategoryId = article.CategoryId;
            this.Tags = article.Tags;
            this.State = article.State;
        }
    }
}
