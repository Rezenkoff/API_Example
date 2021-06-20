using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetCatalogNodeById;
using Autodoc.CatalogAdmin.Domain.Entities;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface ICatalogNodeVmBuilder
    {
        CatalogNodeVm Build (CatalogNode nodes, IEnumerable<CatalogNode> nodeChildren);
    }
}
