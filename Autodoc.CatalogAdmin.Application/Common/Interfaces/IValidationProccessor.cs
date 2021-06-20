using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface IValidationProccessor
    {
        Task<ICatalogValidationResult> StartValidation ();
    }
}
