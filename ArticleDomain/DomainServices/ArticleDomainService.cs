using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainEvents;
using ArticleDomain.IRepositories;
using System;
using Zaaby.DDD.Abstractions.Domain;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

namespace ArticleDomain.DomainServices
{
    /// <summary>
    ///     文章领域服务，封装文章的规则，事件推送
    /// </summary>
    public class ArticleDomainService : IDomainService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IDomainEventPublisher _domainEventPublisher;

        public ArticleDomainService(IArticleRepository articleRepository, IDomainEventPublisher domainEventPublisher)
        {
            _articleRepository = articleRepository;
            _domainEventPublisher = domainEventPublisher;
        }

        public void CreateArticle(Article article)
        {
            var id = _articleRepository.FindIdByTitle(article.Title);
            if (id != null)
                throw new ArticleDomainEntityExistsException($"文章 {article.Title} 已经存在");
            _articleRepository.Add(article);
            _domainEventPublisher.PublishEvent(new NewArticleCreateDomainEvent(article));
        }

        public Article PublishArticle(Guid id, out int version)
        {
            var article = _articleRepository.Restore(id, out version);
            if (article == null)
                throw new ArticleDomainException("文章不存在");
            article.Publish();

            if (!_articleRepository.Update(article, version))
                throw new ArticleDomainException("文章发布失败，版本不一致");

            return article;
        }

        public Article DeleteArticle(Guid id, out int version)
        {
            var article = _articleRepository.Restore(id, out version);
            if (article == null)
                throw new ArticleDomainException("文章不存在");
            article.Delete();
            if (!_articleRepository.Update(article, version))
                throw new ArticleDomainException("文章删除失败，版本不一致");
            return article;
        }
    }
}