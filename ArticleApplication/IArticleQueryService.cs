using IArticleApplication.Model;
using IArticleApplication.Params;

namespace ArticleApplication
{
    /// <summary>
    /// 文章查询服务，主要用来做查询优化
    /// </summary>
    public interface IArticleQueryService
    {
        ArticleDetail GetArticleDetail(string id);
        ArticlePageInfo QueryArticleByPage(QueryArticleParam param);
    }
}
