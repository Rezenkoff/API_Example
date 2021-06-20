using Autodoc.CatalogAdmin.Application.Common.Constants;
using Autodoc.CatalogAdmin.Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.GetArticlesTypesList
{
    public class GetArticlesTypesListQuery : IRequest<IEnumerable<ArticleTypeDto>>
    {

    }

    public class GetNodeWithArticlesQueryHandler : IRequestHandler<GetArticlesTypesListQuery, IEnumerable<ArticleTypeDto>>
    {
        public async Task<IEnumerable<ArticleTypeDto>> Handle (GetArticlesTypesListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(ArticlesTypes.Types);
        }
    }
}
