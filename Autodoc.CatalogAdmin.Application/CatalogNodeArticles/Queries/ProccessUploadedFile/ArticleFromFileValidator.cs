using Autodoc.CatalogAdmin.Application.Common.Constants;
using Autodoc.CatalogAdmin.Application.Common.Models;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.ProccessUploadedFile
{
    public class ArticleFromFileValidator : AbstractValidator<IArticleParseModel>
    {
        public ArticleFromFileValidator ()
        {
            RuleFor(art => art.ArtTypeId).MustAsync(ShouldExists).WithMessage("TypeId doesn`t exist"); 
        }

        public async Task<bool> ShouldExists (IArticleParseModel model, int TypeId, CancellationToken cancellationToken)
        {
            var result = ArticlesTypes.Types.Any(t => t.TypeId == TypeId);

            return await Task.FromResult(result);
        }
    }
}
