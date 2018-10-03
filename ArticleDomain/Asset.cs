namespace ArticleDomain
{
    /// <summary>
    /// 断言工具类
    /// </summary>
    internal static class Assert
    {
        public static void IsNotNull(string name, object obj)
        {
            if (obj == null)
            {
                throw new ArticleDomainArgumentException(string.Format("{0}不能为空", name));
            }
        }

        public static void IsNotNullOrEmpty(string name, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArticleDomainArgumentException(string.Format("{0}不能为空", name));
            }
        }

        public static void IsNotNullOrWhiteSpace(string name, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArticleDomainArgumentException(string.Format("{0}不能为空", name));
            }
        }

        public static void AreEqual(string id1, string id2, string errorMessageFormat)
        {
            if (id1 != id2)
            {
                throw new ArticleDomainArgumentException(string.Format(errorMessageFormat, id1, id2));
            }
        }
    }
}
