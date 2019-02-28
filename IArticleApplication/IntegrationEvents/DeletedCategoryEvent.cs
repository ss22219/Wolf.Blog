using System;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    public class DeletedCategoryEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
    }
}