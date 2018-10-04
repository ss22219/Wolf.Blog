using ArticleDomain.DomainEvents;
using ArticleDomain.IRepositories;
using System;
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
        /// 更新分类文章数，使用PO版本号控制数据一致性
        /// </summary>
        /// <param name="domainEvent"></param>
        public void Handle(NewArticleCreateDomainEvent domainEvent)
        {
            ///版本不正确，采用重试策略
            while (true)
            {
                //直接从仓储还原一个聚合，无法保证唯一性
                var category = _articleCategoryRepository.Restore(domainEvent.CategoryId, out int version);
                if (category != null)
                {
                    category.IncremntArticleQuantity();

                    if (_articleCategoryRepository.Update(category, version))
                        break;
                }
                else
                    break;
            }
        }
    }
}
