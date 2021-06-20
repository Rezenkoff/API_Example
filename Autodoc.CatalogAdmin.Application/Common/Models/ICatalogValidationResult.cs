using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Models
{
    public interface ICatalogValidationResult
    {
        public bool Valid { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorTitle { get; set; }
        public IEnumerable<int> InvalidNodesList { get; set; }
    }
}
