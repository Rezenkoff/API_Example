using Autodoc.CatalogAdmin.Application.Common.Models;

namespace Autodoc.CatalogAdmin.Infrastucture.Models.Articles
{
    public class ArticleParseModel : IArticleParseModel
    {
        public int ArticleId { get; set; }
        public int ArtTypeId { get; set; }
    }
}
