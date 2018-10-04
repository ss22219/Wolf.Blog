using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    public class DeletedCategoryEvent:IIntegrationEvent
    {
        public string Id { get; set; }
    }
}