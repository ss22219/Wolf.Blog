using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainEvents;
using ArticleDomain.IRepositories;
using Zaaby.DDD.Abstractions.Domain;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

namespace ArticleDomain.DomainServices
{
    /// <summary>
    /// 文章领域服务，封装文章的规则，事件推送
    /// </summary>
    public class ArticleDomainService : IDomainService
    {
        IArticleRepository _articleRepository;
        IDomainEventPublisher _domainEventPublisher;
        public ArticleDomainService(IArticleRepository articleRepository, IDomainEventPublisher domainEventPublisher)
        {
            _articleRepository = articleRepository;
            _domainEventPublisher = domainEventPublisher;
        }

        public void CreateArticle(Article article)
        {
            var id = _articleRepository.FindIdByTitle(article.Title);
            if (!string.IsNullOrEmpty(id))
                throw new ArticleDomainEntityExistsException($"文章 {article.Title} 已经存在");
            _articleRepository.Add(article);

            _domainEventPublisher.PublishEvent(new NewArticleCreateDomainEvent(article));
        }
    }
}
