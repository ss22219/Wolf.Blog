using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FileRepository
{
    public class ArticleRepository : IArticleRepository
    {
        public string _baseDir;
        public string SaveDir => _baseDir.TrimEnd(new char[] { '/' }) + "/article/";

        public ArticleRepository(string baseDir)
        {
            _baseDir = baseDir;
            if (!Directory.Exists(SaveDir))
                Directory.CreateDirectory(SaveDir);
        }

        private string GetSavePath(Guid id) => $"{SaveDir}{id}";

        public void Add(Article article)
        {
            if (File.Exists(GetSavePath(article.Id)))
                throw new Exception("文件已经存在");
            File.WriteAllText(GetSavePath(article.Id), JsonConvert.SerializeObject(article));
        }

        public Article Restore(Guid id, out int version)
        {
            version = 0;
            if (!File.Exists(GetSavePath(id)))
                return null;
            return JsonConvert.DeserializeObject<Article>(File.ReadAllText(GetSavePath(id)));
        }

        public bool Update(Article article, int version)
        {
            File.WriteAllText(GetSavePath(article.Id), JsonConvert.SerializeObject(article));
            return true;
        }

        public Guid? FindIdByTitle(string title)
        {
            return null;
        }
    }
}
