using System.Collections.Generic;
using System.ComponentModel;

namespace IArticleApplication.Params
{
    public class CreateArticleParam
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
        public CreateArticleState State { get; private set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IList<string> Tags { get; private set; }
    }

    /// <summary>
    /// 文章状态
    /// </summary>
    public enum CreateArticleState
    {
        [Description("草稿")]
        Draft = 0,
        [Description("已发布")]
        Published = 0
    }
}
