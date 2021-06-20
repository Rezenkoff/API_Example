using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents
{
    public class ParentNodeGroupVm
    {
        public string GroupName { get; set; }
        public IEnumerable<NodeKeyValueDto> GroupedParentNodes { get; set; }
    }
}
