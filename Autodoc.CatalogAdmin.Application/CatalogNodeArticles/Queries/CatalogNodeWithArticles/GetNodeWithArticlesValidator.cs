using Autodoc.CatalogAdmin.Application.Common.Constants;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles
{
    public class GetNodeWithArticlesValidator : AbstractValidator<GetNodeWithArticlesQuery>
    {
        public GetNodeWithArticlesValidator ()
        {
            RuleFor(n => n.TypeId)
                .MustAsync(ShouldExists).WithMessage("TypeId doesn`t exist");
        }

        public async Task<bool> ShouldExists (GetNodeWithArticlesQuery model, int TypeId, CancellationToken cancellationToken)
        {
            var result = ArticlesTypes.Types.Any(t => t.TypeId == TypeId);

            return await Task.FromResult(result);
        }
    }
}
