namespace ArticelDomain.IRepositories
{
    /// <summary>
    /// 文章仓储的接口
    /// </summary>
    public interface IArticelRepository
    {
        string FindByTitle(string title);
    }
}
