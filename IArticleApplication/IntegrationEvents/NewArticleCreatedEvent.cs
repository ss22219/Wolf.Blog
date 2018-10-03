using System.Collections.Generic;
using System.ComponentModel;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    /// <summary>
    /// 新创建文章事件
    /// </summary>
    public class NewArticleCreatedEvent : IIntegrationEvent
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
        public NewArticleCreatedState State { get; private set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IList<string> Tags { get; private set; }

        public NewArticleCreatedEvent(string id, string title, string content, System.DateTime createDate, NewArticleCreatedState state, string categoryId, IList<string> tags = null)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.CategoryId = categoryId;
            this.Tags = tags;
            this.State = state;
        }
    }

    /// <summary>
    /// 文章状态
    /// </summary>
    public enum NewArticleCreatedState
    {
        [Description("草稿")]
        Draft = 0,
        [Description("已发布")]
        Published = 0
    }
}
