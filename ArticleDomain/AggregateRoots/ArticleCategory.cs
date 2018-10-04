using Zaaby.DDD.Abstractions.Domain;

namespace ArticleDomain.AggregateRoots
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory : IAggregateRoot<string>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int ArticleQuantity { get; private set; }

        public ArticleCategory(string id, string name, int articleQuantity)
        {
            Assert.IsNotNullOrWhiteSpace("文章分类id", id);
            Assert.IsNotNullOrWhiteSpace("文章分类名称", name);
            this.Id = id;
            this.Name = name;
            this.ArticleQuantity = articleQuantity;
        }

        public void IncremntArticleQuantity()
        {
            ArticleQuantity++;
        }
    }
}
