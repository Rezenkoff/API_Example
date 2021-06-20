using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetCatalogNodeById
{
    public class CatalogNodeVm
    {
        public CatalogNodeDto ParentNode { get; set; }         
        public IEnumerable<CatalogNodeDto> NodeChildren { get; set; }
    }
}
