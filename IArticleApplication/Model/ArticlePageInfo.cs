using System;
using System.Collections.Generic;
using System.Text;

namespace IArticleApplication.Model
{
    public class ArticlePageInfo
    {
        public List<ArticleDetail> List { get; set; }
        public bool NextPage { get; set; }
    }
}
