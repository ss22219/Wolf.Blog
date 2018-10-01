using IArticelApplication.Model;
using IArticelApplication.Params;
using Zaaby.DDD.Abstractions.Application;

namespace IArticelApplication
{
    /// <summary>
    /// 文章应用服务接口，提供给外部调用的领域外观，屏蔽领域细节
    /// </summary>
    public interface IArticleApplicationService : IApplicationService
    {
        void CreateArticel(CreateArticleParam createArticleParam);

        ArticelDetail FindArticelById(string id);

        ArticelPageInfo QueryArticelByPage(QueryArticelParam param);
    }
}
