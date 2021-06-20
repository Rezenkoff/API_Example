using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models;
using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface ICatalogNodeCommandService
    {
        Task WorkWithModifiedNode(List<CatalogNodeDto> nodes, ActionTypeEnum type, CancellationToken cancellationToken);
        Task<int> CreateNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken);
        Task UpdateNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken);
        Task DeleteNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken);
        Task MoveNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken);
    }
}
