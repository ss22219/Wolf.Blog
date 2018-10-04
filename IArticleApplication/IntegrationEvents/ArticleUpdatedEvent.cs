using IArticleApplication.Model;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    public class ArticleUpdatedEvent :IIntegrationEvent
    {
        public ArticleEventData Data { get; set; }
    }
}