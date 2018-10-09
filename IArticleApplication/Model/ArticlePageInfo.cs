using System.Collections.Generic;

namespace IArticleApplication.Model
{
    public class ArticlePageInfo
    {
        public List<ArticleDetail> List { get; set; }
        public bool NextPage { get; set; }
    }
}