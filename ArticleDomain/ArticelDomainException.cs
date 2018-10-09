using System;

namespace ArticleDomain
{
    /// <summary>
    ///     文章领域异常，用于区分业务异常与Bug异常
    /// </summary>
    public class ArticleDomainException : Exception
    {
        public ArticleDomainException(string message) : base(message)
        {
        }
    }

    public class ArticleDomainArgumentException : ArticleDomainException
    {
        public ArticleDomainArgumentException(string message) : base(message)
        {
        }
    }

    public class ArticleDomainEntityExistsException : ArticleDomainException
    {
        public ArticleDomainEntityExistsException(string message) : base(message)
        {
        }
    }
}