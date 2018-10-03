using ArticleDomain;
using ArticleDomain.AggregateRoots;
using ArticleDomain.DomainEvents;
using ArticleDomain.DomainServices;
using MemoryRepository;
using Moq;
using System;
using Xunit;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;

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
        public void CreateNewArticle()
        {
            var article = CreateAggregateRoot();
            var repos = new ArticleRepository();
            var mock = new Mock<IDomainEventPublisher>();
            var publishedEvent = false;

            mock.Setup(p => p.PublishEvent<NewArticleCreateDomainEvent>(It.IsAny<NewArticleCreateDomainEvent>()))
                .Callback<NewArticleCreateDomainEvent>(
                e =>
                {
                    Assert.Equal(article.Id, e.Id);
                    Assert.Equal(article.Content, e.Content);
                    Assert.Equal(article.Title, e.Title);
                    Assert.Equal(article.CategoryId, e.CategoryId);
                    Assert.Equal(article.State, e.State);
                    publishedEvent = true;
                }
            );
            var fackPublisher = mock.Object;

            var service = new ArticleDomainService(repos, fackPublisher);
            service.CreateArticle(article);

            publishedEvent = false;
            try
            {
                service.CreateArticle(article);
                throw new Exception();
            }
            catch (ArticleDomainException)
            {
            }
            Assert.False(publishedEvent);
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
