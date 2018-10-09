using System;
using System.Collections.Generic;
using IArticleApplication.Model;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    /// <summary>
    ///     新创建文章事件
    /// </summary>
    public class NewArticleCreatedEvent : IIntegrationEvent
    {
        public NewArticleCreatedEvent()
        {
        }

        public NewArticleCreatedEvent(string id, string title, string content, DateTime createDate,
            ArticleDetailState state, string categoryId, IList<string> tags = null)
        {
            Data = new ArticleEventData
            {
                Id = id,
                Title = title,
                Content = content,
                CategoryId = categoryId,
                Tags = tags,
                State = state,
                CreateDate = createDate
            };
        }

        public ArticleEventData Data { get; set; }
    }
}