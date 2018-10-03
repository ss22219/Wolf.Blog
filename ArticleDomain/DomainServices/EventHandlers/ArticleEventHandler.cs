﻿using ArticleDomain.DomainEvents;
using ArticleDomain.IRepositories;
using System;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticleDomain.DomainServices.EventHandlers
{
    public class ArticleEventHandler : IDomainEventHandler<NewArticleCreateDomainEvent>
    {
        IArticleCategoryRepository _articleCategoryRepository;
        public ArticleEventHandler(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        /// 更新分类文章数，使用PO版本号控制数据一致性
        /// </summary>
        /// <param name="domainEvent"></param>
        public void Handle(NewArticleCreateDomainEvent domainEvent)
        {
            ///版本不正确，采用重试策略
            while (true)
            {
                var category = _articleCategoryRepository.Restore(domainEvent.CategoryId, out int version);
                if (category != null)
                {
                    category.IncremntArticleQuantity();

                    if (_articleCategoryRepository.Update(category, version))
                        break;
                }
            }
        }
    }
}