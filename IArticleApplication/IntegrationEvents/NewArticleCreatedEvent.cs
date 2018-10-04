using System;
using System.Collections.Generic;
using System.ComponentModel;
using IArticleApplication.Model;
using Zaaby.DDD.Abstractions.Application;

namespace IArticleApplication.IntegrationEvents
{
    /// <summary>
    /// 新创建文章事件
    /// </summary>
    public class NewArticleCreatedEvent : IIntegrationEvent
    {
        public ArticleDetail Data { get;  set; }

        public NewArticleCreatedEvent()
        {
        }

        public NewArticleCreatedEvent(string id, string title, string content, DateTime createDate, ArticleDetailState state, string categoryId, IList<string> tags = null)
        {
            this.Data = new ArticleDetail();
            this.Data.Id = id;
            this.Data.Title = title;
            this.Data.Content = content;
            this.Data.CategoryId = categoryId;
            this.Data.Tags = tags;
            this.Data.State = state;
            this.Data.CreateDate = createDate;
        }
    }
}
