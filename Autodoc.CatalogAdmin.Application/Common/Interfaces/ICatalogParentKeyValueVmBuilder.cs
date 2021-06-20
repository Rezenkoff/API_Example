using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents;
using Autodoc.CatalogAdmin.Domain.Entities;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface ICatalogParentKeyValueVmBuilder
    {
        IEnumerable<ParentNodeGroupVm> Build (IEnumerable<CatalogNode> nodesList);
    }
}
