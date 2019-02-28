using System;

namespace BlogWeb.QueryService.Dtos.Res
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ArticleQuantity { get; internal set; }
    }
}
