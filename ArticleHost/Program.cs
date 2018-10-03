﻿using ArticleApplication;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using IArticleApplication;
using Infrastracture.Configuration.Abstractions;
using MemoryRepository;
using Microsoft.Extensions.Configuration;
using QueryService;
using System;
using System.IO;
using Wolf.Configuration;
using Zaabee.RabbitMQ;
using Zaabee.RabbitMQ.Abstractions;
using Zaabee.RabbitMQ.Jil;
using Zaaby.DDD;
using Zaaby.DDD.Abstractions.Application;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using Zaaby.DDD.EventBus.RabbitMQ;

namespace BlogHost
{
    class Program
    {
        static void Main(string[] args)
        {

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("RabbitMQ.json", true, true)
                .AddJsonFile("Application.json", true, true);
            var config = configBuilder.Build();

            var rabbitMqConfig = config.GetSection("ZaabeeRabbitMQ").Get<MqConfig>();
            var applicationConfig = config.GetSection("Application");

            Zaaby.ZaabyServer.GetInstance()
                .UseDDD()
                .UseZaabyServer<IApplicationService>()
                .UseEventBus()
                .AddSingleton<IConfig>(p =>
                   new Config(applicationConfig))
                .AddSingleton<IZaabeeRabbitMqClient>(p =>
                    new ZaabeeRabbitMqClient(rabbitMqConfig, new Serializer()))
                .AddScoped<IIntegrationEventBus, ZaabyEventBus>()
                .AddSingleton<IArticleRepository, ArticleRepository>()
                .AddSingleton<ArticleRepository>()
                .AddSingleton<IArticleCategoryRepository, ArticleCategoryRepository>()
                .AddSingleton<IArticleQueryService, ArticleQueryService>()
                .UseUrls("http://*:5001")
                .Run();
        }
    }
}