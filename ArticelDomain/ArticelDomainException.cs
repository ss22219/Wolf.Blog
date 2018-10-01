using System;

namespace ArticelDomain
{
    /// <summary>
    /// 文章领域异常，用于区分业务异常与Bug异常
    /// </summary>
    public class ArticelDomainException : Exception
    {
        public ArticelDomainException(string message) : base(message)
        {
        }
    }

    public class ArticelDomainArgumentException : ArgumentException
    {
        public ArticelDomainArgumentException(string message) : base(message)
        {
        }

        public ArticelDomainArgumentException(string message, string paramName) : base(message, paramName)
        {
        }
    }

    public class ArticelDomainEntityExistsException : Exception
    {
        public ArticelDomainEntityExistsException(string message) : base(message)
        {
        }
    }
}