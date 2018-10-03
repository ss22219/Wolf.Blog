using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainServices;
using MemoryRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Zaaby.DDD;

namespace UnitTest
{
    public class ArticleTest
    {
        [Fact]
        public Article CreateAggregateRoot()
        {
            return new Article(Guid.NewGuid().ToString(), "test", "test", DateTime.Now, Article.ArticleState.Draft);
        }

        [Fact]
        public void Persistance()
        {
            var article = CreateAggregateRoot();
            new ArticleRepository().Add(article);
        }

        [Fact]
        public Article Restore()
        {
            var article = CreateAggregateRoot();
            var repos = new ArticleRepository();
            repos.Add(article);
            article = repos.Restore(article.Id, out int version);
            Assert.NotNull(article);
            return article;
        }

        [Fact]
        public void Update()
        {
            var article = CreateAggregateRoot();
            var repos = new ArticleRepository();
            repos.Add(article);

            article = repos.Restore(article.Id, out int version);
            article.Publish();

            Assert.True(repos.Update(article, version));

            Assert.False(repos.Update(article, version));
        }

    }


}
