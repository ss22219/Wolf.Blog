using System.Collections.Generic;
using IArticleApplication.Model;

namespace ArticleApplication
{
    public interface ICategoryQueryService
    {
        IList<CategoryInfo> AllCategory();
    }
}