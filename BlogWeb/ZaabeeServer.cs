using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Zaaby.Abstractions;
using Zaaby.DDD;
using Zaaby.DDD.EventBus.RabbitMQ;

namespace BlogWeb
{
    public class ZaabyServer : IZaabyServer
    {
        private static List<Type> _allTypes;
        internal static readonly List<Type> ServiceRunnerTypes = new List<Type>();
        private IServiceCollection _services;
        static ZaabyServer()
        {
            _allTypes = Directory.GetFiles(Path.GetDirectoryName(typeof(Startup).Assembly.Location), "*.dll").Select(file => Assembly.LoadFile(file)).SelectMany(assembly => assembly.GetTypes()).ToList();
        }
        public ZaabyServer(IServiceCollection services)
        {
            _services = services;
        }

        public static IZaabyServer UseDDD(IServiceCollection services)
        {
            return new ZaabyServer(services).
                UseDDD().
                UseDomainService().
                UseIntegrationEventHandler().
                UseDomainEventHandler().
                UseEventBus();
        }

        public List<Type> AllTypes { get => _allTypes; set => throw new NotImplementedException(); }

        public IZaabyServer AddScoped(Type serviceType, Type implementationType)
        {
            _services.AddScoped(serviceType, implementationType);
            return this;
        }

        public IZaabyServer AddScoped(Type serviceType)
        {
            _services.AddScoped(serviceType);
            return this;
        }

        public IZaabyServer AddScoped<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public IZaabyServer AddScoped(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            throw new NotImplementedException();
        }

        public IZaabyServer AddScoped<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            throw new NotImplementedException();
        }

        public IZaabyServer AddSingleton(Type serviceType, Type implementationType)
        {
            _services.AddSingleton(serviceType, implementationType);
            return this;
        }

        public IZaabyServer AddSingleton(Type serviceType)
        {
            _services.AddSingleton(serviceType);
            return this;
        }

        public IZaabyServer AddSingleton<TService>() where TService : class
        {
            _services.AddSingleton<TService>();
            return this;
        }

        public IZaabyServer AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            _services.AddSingleton(serviceType, implementationFactory);
            return this;
        }

        public IZaabyServer AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            _services.AddSingleton(implementationFactory);
            return this;
        }

        public IZaabyServer AddTransient(Type serviceType, Type implementationType)
        {
            _services.AddSingleton(serviceType, implementationType);
            return this;
        }

        public IZaabyServer AddTransient(Type serviceType)
        {
            _services.AddSingleton(serviceType);
            return this;
        }

        public IZaabyServer AddTransient<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public IZaabyServer AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            throw new NotImplementedException();
        }

        public IZaabyServer AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            throw new NotImplementedException();
        }

        public IZaabyServer RegisterServiceRunners(List<Type> runnerTypes)
        {
            runnerTypes.ForEach(type => RegisterServiceRunner(type));
            return this;
        }

        public IZaabyServer RegisterServiceRunners(Dictionary<Type, Type> runnerTypes)
        {
            foreach (var (key, value) in runnerTypes)
                RegisterServiceRunner(key, value);
            return this;
        }

        public IZaabyServer RegisterServiceRunner(Type runnerType)
        {
            AddSingleton(runnerType);
            ServiceRunnerTypes.Add(runnerType);
            return this;
        }

        public IZaabyServer RegisterServiceRunner(Type serviceType, Type implementationType)
        {
            AddSingleton(serviceType, implementationType);
            ServiceRunnerTypes.Add(serviceType);
            return this;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public IZaabyServer UseUrls(params string[] urls)
        {
            throw new NotImplementedException();
        }

        public IZaabyServer UseZaabyServer<TService>()
        {
            throw new NotImplementedException();
        }

        public IZaabyServer UseZaabyServer(Func<Type, bool> definition)
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddScoped<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddScoped<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddSingleton<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddTransient<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        IZaabyServer IZaabyServer.AddTransient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
        {
            throw new NotImplementedException();
        }
    }
}
