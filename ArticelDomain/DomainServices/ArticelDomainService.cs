using ArticelDomain.AggregateRoots;
using ArticelDomain.IRepositories;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticelDomain.DomainServices
{
    public class ArticelDomainService : IDomainService
    {
        IArticelRepository _articelRepository;
        public ArticelDomainService(IArticelRepository articelRepository)
        {
            _articelRepository = articelRepository;
        }

        public void CreateArticel(Article articel)
        {
            var id = _articelRepository.FindByTitle(articel.Title);
            if (!string.IsNullOrEmpty(id))
                throw new ArticelDomainEntityExistsException($"文章 {articel.Title} 已经存在");
        }
    }
}
