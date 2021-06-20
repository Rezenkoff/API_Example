using Autodoc.CatalogAdmin.Domain.Enums;

namespace Autodoc.CatalogAdmin.Domain.Entities
{
    public class CategoryArticle
    {
        public int ArticleId { get; set; }
        public ArticleTypeEnum ArticleType { get; set; }
        public string ArticleName { get; set; }
        public bool ArticleIsActive { get; set; }
        public int ArticleSupplierId { get; set; }
        public string CardNumber { get; set; }
        public string ArticleInfo { get; set; }
        public string Measure { get; set; }
        public string ArticleNameUkr { get; set; }
    }
}
