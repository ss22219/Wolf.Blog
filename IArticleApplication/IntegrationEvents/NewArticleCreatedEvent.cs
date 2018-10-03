using System;
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
        public string Id { get;  set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get;  set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get;  set; }

        /// <summary>
        /// 文章分类ID
        /// </summary>
        public string CategoryId { get;  set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public NewArticleCreatedState State { get;  set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get;  set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IList<string> Tags { get;  set; }

        public NewArticleCreatedEvent()
        {
        }

        public NewArticleCreatedEvent(string id, string title, string content, DateTime createDate, NewArticleCreatedState state, string categoryId, IList<string> tags = null)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.CategoryId = categoryId;
            this.Tags = tags;
            this.State = state;
            this.CreateDate = createDate;
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
