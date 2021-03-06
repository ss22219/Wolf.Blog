﻿using ArticleApplication;
using ArticleDomain.DomainServices;
using ArticleDomain.IRepositories;
using BlogWeb.QueryService;
using FileRepository;
using IArticleApplication;
using Infrastracture.Configuration;
using Infrastracture.Configuration.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Zaabee.RabbitMQ;
using Zaabee.RabbitMQ.Abstractions;
using Zaabee.RabbitMQ.Jil;
using Zaaby.DDD;
using Zaaby.DDD.Abstractions.Infrastructure.EventBus;
using Zaaby.DDD.EventBus.RabbitMQ;

namespace BlogWeb
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("RabbitMQ.json", true, true)
                .AddJsonFile("Application.json", true, true).
            Build();
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            var rabbitMqConfig = Configuration.GetSection("ZaabeeRabbitMQ").Get<MqConfig>();

            ZaabyServer.UseDDD(services);
            var config = new Config(Configuration.GetSection("Application"));
            var dataDir = Environment.GetEnvironmentVariable("DATA_DIR") ?? "./";
            services
                .AddSingleton<IZaabeeRabbitMqClient>(p =>
                    new ZaabeeRabbitMqClient(rabbitMqConfig, new Serializer()))
                .AddScoped<IIntegrationEventBus, ZaabyEventBus>()
                .AddScoped<IDomainEventPublisher, DomainEventPublisher>()
                .AddScoped<ArticleDomainService>()
                .AddScoped<ArticleCategoryDomainService>()
                .AddSingleton<DomainEventHandlerProvider>()
                .AddSingleton<IArticleRepository>(new ArticleRepository(dataDir))
                .AddSingleton<IArticleCategoryRepository>(new ArticleCategoryRepository(dataDir))
                .AddSingleton<CategoryQueryService, CategoryQueryService>()
                .AddScoped<IArticleApplicationService, ArticleApplicationService>()
                .AddSingleton(new ArticleQueryService(config))
                .AddSingleton<CategoryQueryService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            ZaabyServer.ServiceRunnerTypes.ForEach(type => serviceProvider.GetService(type));
            app.UseCors(builder =>
            {
                builder.WithOrigins("*");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseRewriter(new RewriteOptions { }.AddRewrite(@"^article/[\d\-a-z]{36}$", "/", true));
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseErrorHandling();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}