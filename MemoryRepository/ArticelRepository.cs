﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;

namespace MemoryRepository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly List<ArticleEntity> articleEntities = new List<ArticleEntity>();

        public void Add(Article article)
        {
            articleEntities.Add(new ArticleEntity(article));
        }

        public string FindIdByTitle(string title)
        {
            return articleEntities.FirstOrDefault(a => a.Title == title)?.Id;
        }

        public Article Restore(string id, out int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.Id == id);
            version = entity?.Version ?? 0;
            if (entity == null)
                return null;
            return new Article(entity.Id, entity.Title, entity.Content, entity.CreateDate,
                (Article.ArticleState) entity.State, entity.CategoryId, entity.Tags);
        }

        public bool Update(Article article, int version)
        {
            var entity = articleEntities.FirstOrDefault(e => e.Id == article.Id);
            if (entity == null)
            {
                entity = new ArticleEntity(article);
                entity.Version = ++version;
                articleEntities.Add(entity);
                return true;
            }

            if (entity.Version != version)
                return false;

            entity.Update(article);
            entity.Version++;
            return true;
        }

        public List<ArticleEntity> GetAllEntity()
        {
            return articleEntities;
        }
    }

    public class ArticleEntity
    {
        public ArticleEntity(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            CategoryId = article.CategoryId;
            State = (int) article.State;
            Tags = article.Tags;
            CreateDate = article.CreateDate;
        }

        public string Id { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     文章分类ID
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        ///     文章状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     文章标签
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     版本
        /// </summary>
        public int Version { get; set; }

        public void Update(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            CategoryId = article.CategoryId;
            State = (int) article.State;
            Tags = article.Tags;
            CreateDate = article.CreateDate;
        }
    }
}