using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles
{
    public class CatalogArticlesResponseVm
    {
        public IEnumerable<CategoryArticleDto> Articles { get; set; }         
        public int Total { get; set; }
    }
}
