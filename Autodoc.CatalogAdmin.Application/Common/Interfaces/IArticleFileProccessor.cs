using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface IArticleFileProccessor
    {
        Task<bool> ProccessFile (string fileName, int categoryId);
    }
}
