using IArticleApplication.Model;
using IArticleApplication.Params;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication
{
    /// <summary>
    /// 文章应用服务接口，提供给外部调用的领域外观，屏蔽领域细节
    /// </summary>
    public interface IArticleApplicationService : IApplicationService
    {
        void CreateArticle(CreateArticleParam createArticleParam);

        ArticleDetail FindArticleById(string id);

        ArticlePageInfo QueryArticleByPage(QueryArticleParam param);
        void Publish(string id);
        void Delete(string id);
    }
}
