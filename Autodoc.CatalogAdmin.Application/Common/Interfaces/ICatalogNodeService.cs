using Autodoc.CatalogAdmin.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface ICatalogNodeService
    {
        Task<CatalogNode> GetCatalogNodeById (int nodeId);
        Task<IEnumerable<CatalogNode>> GetCatalogNodeChildrenById (int nodeId);
        Task<IEnumerable<CatalogNode>> GetCategoryTreeForUpdate();
    }
}
