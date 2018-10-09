using System.IO;
using ArticleApplication;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using Infrastracture.Configuration;
using Infrastracture.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;
using MongoRepository;
using QueryService;
using Zaabee.Mongo;
using Zaabee.Mongo.Common;
using Zaabee.RabbitMQ;
using Zaabee.RabbitMQ.Abstractions;
using Zaabee.RabbitMQ.Jil;
using Zaaby;
using Zaaby.DDD;
using Zaaby.DDD.Abstractions.Application;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using Zaaby.DDD.EventBus.RabbitMQ;

namespace ArticleHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("RabbitMQ.json", true, true)
                .AddJsonFile("MongoDb.json", true, true)
                .AddJsonFile("Application.json", true, true);
            var config = configBuilder.Build();

            var rabbitMqConfig = config.GetSection("ZaabeeRabbitMQ").Get<MqConfig>();
            var mongoConfig = config.GetSection("MongoDb").Get<MongoDbConfiger>();

            var applicationConfig = config.GetSection("Application");

            var mongoClient = new ZaabeeMongoClient(mongoConfig);
            var categoryRepository = new ArticleCategoryRepository(mongoClient);
            var articleRepository = new ArticleRepository(mongoClient);

            ZaabyServer.GetInstance()
                .UseDDD()
                .UseZaabyServer<IApplicationService>()
                .UseEventBus()
                .AddSingleton<IConfig>(p =>
                    new Config(applicationConfig))
                .AddSingleton<IZaabeeRabbitMqClient>(p =>
                    new ZaabeeRabbitMqClient(rabbitMqConfig, new Serializer()))
                .AddScoped<IIntegrationEventBus, ZaabyEventBus>()
                .AddScoped<ArticleCategoryDomainService>()
                .AddSingleton<IArticleQueryService, ArticleQueryService>()
                .AddSingleton<IArticleRepository>(e => articleRepository)
                .AddSingleton(e => articleRepository)
                .AddSingleton<IArticleCategoryRepository>(e => categoryRepository)
                .AddSingleton<ICategoryQueryService>(e => categoryRepository)
                .Run();
        }
    }
}