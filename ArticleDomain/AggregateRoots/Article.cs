using System;
using System.Collections.Generic;
using System.ComponentModel;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticleDomain.AggregateRoots
{
    /// <summary>
    /// 文章聚合根
    /// </summary>
    public class Article : IAggregateRoot<string>
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

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        public Article(string id, string title, string content, DateTime createDate, ArticleState state, string categoryId = null, IList<string> tags = null)
        {
            Assert.IsNotNullOrWhiteSpace("文章id", id);
            Assert.IsNotNullOrWhiteSpace("文章标题", title);
            Assert.IsNotNullOrWhiteSpace("文章内容", content);
            Assert.IsNotNull("文章创建日期", createDate);

            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.CategoryId = categoryId;
            this.Tags = tags ?? new List<string>();
            this.State = state;
            this.CreateDate = createDate;
        }

        public void Publish()
        {
            State = ArticleState.Published;
        }

        public enum ArticleState
        {
            [Description("草稿")]
            Draft = 0,
            [Description("已发布")]
            Published = 0,
            [Description("已删除")]
            Deleted = 0
        }
    }
}
