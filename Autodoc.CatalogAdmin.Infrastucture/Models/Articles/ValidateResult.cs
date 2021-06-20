using Autodoc.CatalogAdmin.Application.Common.Models;

namespace Autodoc.CatalogAdmin.Infrastucture.Models.Articles
{
    public class ValidateResult : IValidateResult
    {
        public bool ValidationSuccess { get; set; }
    }
}
