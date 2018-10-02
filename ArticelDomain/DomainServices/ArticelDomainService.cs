using ArticelDomain.AggregateRoots;
using ArticelDomain.DomainEvents;
using ArticelDomain.IRepositories;
using Zaaby.DDD.Abstractions.Domain;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

namespace ArticelDomain.DomainServices
{
    /// <summary>
    /// 文章领域服务，封装文章的规则，事件推送
    /// </summary>
    public class ArticelDomainService : IDomainService
    {
        IArticelRepository _articelRepository;
        IDomainEventPublisher _domainEventPublisher;
        public ArticelDomainService(IArticelRepository articelRepository, IDomainEventPublisher domainEventPublisher)
        {
            _articelRepository = articelRepository;
            _domainEventPublisher = domainEventPublisher;
        }

        public void CreateArticel(Article articel)
        {
            var id = _articelRepository.FindIdByTitle(articel.Title);
            if (!string.IsNullOrEmpty(id))
                throw new ArticelDomainEntityExistsException($"文章 {articel.Title} 已经存在");
            _articelRepository.Add(articel);

            _domainEventPublisher.PublishEvent(new NewArticelCreateDomainEvent(articel));
        }
    }
}
