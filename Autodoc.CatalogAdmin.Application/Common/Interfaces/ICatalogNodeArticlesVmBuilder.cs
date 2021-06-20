using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles;
using Autodoc.CatalogAdmin.Domain.Entities;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface ICatalogNodeArticlesVmBuilder
    {
        CatalogArticlesResponseVm Build (IEnumerable<CategoryArticle> articles, int total);
    }
}
