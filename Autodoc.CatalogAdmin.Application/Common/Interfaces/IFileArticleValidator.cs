using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface IFileArticleValidator
    {
        Task<IValidateResult> ValidateExisting (IEnumerable<IArticleParseModel> articles);
    }
}
