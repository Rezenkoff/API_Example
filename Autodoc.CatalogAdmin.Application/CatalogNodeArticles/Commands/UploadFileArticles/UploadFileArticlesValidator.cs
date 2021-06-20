using FluentValidation;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Commands.UploadFileArticles
{
    class UploadFileArticlesValidator : AbstractValidator<UploadFileArticlesCommand>
    {
        public UploadFileArticlesValidator ()
        {
            RuleFor(n => n.Files.Count).GreaterThan(0).WithMessage("No files found");
        }
    }
}
