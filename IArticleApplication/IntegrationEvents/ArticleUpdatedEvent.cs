using IArticleApplication.Model;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    public class ArticleUpdatedEvent :IIntegrationEvent
    {
        public ArticleDetail NewValues { get; set; }
        public ArticleDetail OldValues { get; set; }
    }
}