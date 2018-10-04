using ArticleDomain.AggregateRoots;

namespace ArticleDomain.IRepositories
{
    public interface IArticleCategoryRepository
    {
        void Add(ArticleCategory article);

        string FindIdByName(string name);

        /// <summary>
        /// 更新持久化状态，如果版本不正确，返回false，如果仓储异常，抛出异常
        /// </summary>
        /// <param name="category"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        bool Update(ArticleCategory category, int version);

        ArticleCategory Restore(string id, out int version);
        void Delete(string id);
    }
}
