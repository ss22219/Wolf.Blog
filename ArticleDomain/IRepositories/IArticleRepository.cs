using ArticleDomain.AggregateRoots;
using System;

namespace ArticleDomain.IRepositories
{
    /// <summary>
    ///     文章仓储的接口
    /// </summary>
    public interface IArticleRepository
    {
        Guid? FindIdByTitle(string title);
        void Add(Article article);

        /// <summary>
        ///     使用版本号更新文章聚合实例
        /// </summary>
        /// <param name="article"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        bool Update(Article article, int version);

        /// <summary>
        ///     还原文章聚合实例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Article Restore(Guid id, out int version);
    }
}