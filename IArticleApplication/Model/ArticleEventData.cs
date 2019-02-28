using System;
using System.Collections.Generic;

namespace IArticleApplication.Model
{
    public class ArticleEventData
    {
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
        public ArticleDetailState State { get; set; }

        /// <summary>
        ///     文章标签
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        public int Version { get; set; }
    }
}