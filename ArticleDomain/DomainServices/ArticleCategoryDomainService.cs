using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using System;

namespace ArticleDomain.DomainServices
{
    public class ArticleCategoryDomainService
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryDomainService(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        public void Delete(Guid id)
        {
            var category = _articleCategoryRepository.Restore(id, out var version);
            if (category == null)
                throw new ArticleDomainException("文章分类不存在");
            _articleCategoryRepository.Delete(id);
        }

        public void Create(ArticleCategory category)
        {
            var id = _articleCategoryRepository.FindIdByName(category.Name);
            if (id != null)
                throw new ArticleDomainEntityExistsException("文章分类已存在");
            _articleCategoryRepository.Add(category);
        }
    }
}