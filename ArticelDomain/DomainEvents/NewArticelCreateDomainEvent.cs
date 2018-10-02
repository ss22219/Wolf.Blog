using ArticelDomain.AggregateRoots;
using System.Collections.Generic;
using Zaaby.DDD.Abstractions.Domain;
using static ArticelDomain.AggregateRoots.Article;

namespace ArticelDomain.DomainEvents
{
    public class NewArticelCreateDomainEvent : IDomainEvent
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

        public NewArticelCreateDomainEvent(Article articel)
        {
            this.Id = articel.Id;
            this.Title = articel.Title;
            this.Content = articel.Content;
            this.CategoryId = articel.CategoryId;
            this.Tags = articel.Tags;
            this.State = articel.State;
        }
    }
}
