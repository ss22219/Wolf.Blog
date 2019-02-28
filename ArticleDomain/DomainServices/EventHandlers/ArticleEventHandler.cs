using ArticleDomain.DomainEvents;
using ArticleDomain.IRepositories;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticleDomain.DomainServices.EventHandlers
{
    public class ArticleEventHandler : IDomainEventHandler<NewArticleCreateDomainEvent>
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleEventHandler(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        ///     更新分类文章数，使用PO版本号控制数据一致性
        /// </summary>
        /// <param name="domainEvent"></param>
        public void Handle(NewArticleCreateDomainEvent domainEvent)
        {
            int version = 0;
            var category = domainEvent.CategoryId == null ? null : _articleCategoryRepository.Restore(domainEvent.CategoryId.Value, out version);
            if (category == null)
                return;
            category.IncremntArticleQuantity();
            _articleCategoryRepository.Update(category, version);
        }
    }
}