using System;

namespace Infrastracture.Configuration.Abstractions
{
    public interface IConfig
    {
        string this[string key] { get; }
        T Get<T>(string key);
    }
}
