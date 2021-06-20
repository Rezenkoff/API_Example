using Autodoc.CatalogAdmin.Application.Common.Enums;
using System;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Queries.GetCatalogTreeState
{
    public class CatalogTreeStateVm
    {
        public CatalogModeEnum Mode { get; set; }
        public int CatalogVersion { get; set; }
        public int BackupVersion { get; set; }
        public int CatalogDboVersion { get; set; }
    }
}
