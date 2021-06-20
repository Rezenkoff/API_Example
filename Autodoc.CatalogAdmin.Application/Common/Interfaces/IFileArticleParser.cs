using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface IFileArticleParser
    {
        IEnumerable<IArticleParseModel> ParseFile (string fileName);
    }
}
