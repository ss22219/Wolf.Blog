using System;

namespace Configuration.Abstractions
{
    public interface IConfiguration
    {
        string this[string key] { get; }
        T Get<T>(string key);
    }
}
