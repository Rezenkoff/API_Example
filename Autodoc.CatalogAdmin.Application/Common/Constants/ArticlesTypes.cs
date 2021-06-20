using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Constants
{
    public static class ArticlesTypes
    {
        public static IEnumerable<ArticleTypeDto> Types
        {
            get
            {
                return new ArticleTypeDto[]
                {
                    new ArticleTypeDto() { TypeId = 0, TypeName = "Легковой" },
                    new ArticleTypeDto() { TypeId = 1, TypeName = "Грузовой" },
                    new ArticleTypeDto() { TypeId = 2, TypeName = "Универсальный" }
                };
            }
        }
    }
}
