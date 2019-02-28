using ArticleDomain.AggregateRoots;
using ArticleDomain.IRepositories;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FileRepository
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        public string _baseDir;
        public string SaveDir => _baseDir.TrimEnd(new char[] { '/' }) + "/category/";
        public string ListFile => $"{SaveDir}list.txt";
        public ArticleCategoryRepository(string baseDir)
        {
            _baseDir = baseDir;
            if (!Directory.Exists(SaveDir))
                Directory.CreateDirectory(SaveDir);
        }

        private string GetSavePath(Guid id) => $"{SaveDir}{id}";

        public void Add(ArticleCategory article)
        {
            if (File.Exists(GetSavePath(article.Id)))
                throw new Exception("文件已经存在");
            File.WriteAllText(GetSavePath(article.Id), JsonConvert.SerializeObject(article));
        }

        public void Delete(Guid id)
        {
            if (File.Exists(GetSavePath(id)))
                File.Delete(GetSavePath(id));
        }

        public Guid? FindIdByName(string name)
        {
            return null;
        }

        public ArticleCategory Restore(Guid id, out int version)
        {
            version = 0;
            if (!File.Exists(GetSavePath(id)))
                return null;
            return JsonConvert.DeserializeObject<ArticleCategory>(File.ReadAllText(GetSavePath(id)));
        }

        public bool Update(ArticleCategory category, int version)
        {
            File.WriteAllText(GetSavePath(category.Id), JsonConvert.SerializeObject(category));
            return true;
        }
    }
}
