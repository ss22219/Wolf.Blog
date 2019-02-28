using IArticleApplication.Params;
using System;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication
{
    /// <summary>
    ///     文章应用服务接口，提供给外部调用的领域外观，屏蔽领域细节
    /// </summary>
    public interface IArticleApplicationService : IApplicationService
    {
        void CreateArticle(CreateArticleParam createArticleParam);
        void PublishArticle(Guid id);
        void DeleteArticle(Guid id);
        void DeleteCategory(Guid id);
        void CreateCategory(CreateCategoryParam param);
    }
}