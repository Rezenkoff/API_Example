
using Autodoc.CatalogAdmin.Domain.Enums;

namespace Autodoc.CatalogAdmin.Domain.Entities
{
    public class CatalogNode
    {
        public CatalogLevel Level { get; set; } = CatalogLevel.First;
        public string NameRus { get; set; }
        public string NameUkr { get; set; }
        public int NodeId { get; set; }
        public int? ParentId { get; set; }
        public string ImageUrl { get; set; }
        public int Vogue { get; set; }
        public bool IsDeleted { get; set; }
        public int ArticlesCount { get; set; }
    }
}
