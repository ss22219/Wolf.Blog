using System;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    public class NewCreatedCategoryEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}