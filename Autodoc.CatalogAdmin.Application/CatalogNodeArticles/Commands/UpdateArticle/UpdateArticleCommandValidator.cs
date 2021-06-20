using FluentValidation;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Commands.UpdateArticle
{
    public class UpdateArticleCommandValidator :  AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(t => t.ArticleName).NotNull();
        }
    }
}
