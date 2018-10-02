using ArticelDomain.AggregateRoots;

namespace ArticelDomain.IRepositories
{
    /// <summary>
    /// 文章仓储的接口
    /// </summary>
    public interface IArticelRepository
    {
        string FindIdByTitle(string title);
        void Add(Article articel);

        /// <summary>
        /// 使用版本号更新文章聚合实例
        /// </summary>
        /// <param name="articel"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        bool Update(Article articel, int version);

        /// <summary>
        /// 还原文章聚合实例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Article Restore(string id, out int version);
    }
}
