using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IArticleApplication.Model
{
    public class ArticleDetail
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
        public ArticleDetailState State { get;  set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IList<string> Tags { get;  set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    public enum ArticleDetailState
    {
        [Description("草稿")]
        Draft = 0,
        [Description("已发布")]
        Published = 0,
        [Description("已删除")]
        Deleted = 0
    }
}
