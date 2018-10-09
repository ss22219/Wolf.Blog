﻿using Infrastracture.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Infrastracture.Configuration
{
    public class Config : IConfig
    {
        private readonly IConfigurationSection _section;

        public Config(IConfigurationSection section)
        {
            _section = section;
        }

        public string this[string key] => _section.GetSection(key)?.Value;

        public T Get<T>(string key)
        {
            return _section.GetSection(key).Get<T>();
        }
    }
}