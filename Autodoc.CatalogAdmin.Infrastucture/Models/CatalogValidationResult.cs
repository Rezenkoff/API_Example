using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Infrastucture.Models
{
    public class CatalogValidationResult : ICatalogValidationResult
    {
        public bool Valid { get; set; }
        public string ErrorDescription { get; set; }
        public IEnumerable<int> InvalidNodesList { get; set; }
        public IEnumerable<string> InvalidNodesNameList { get; set; }
        public string ErrorTitle { get; set; }
    }
}
