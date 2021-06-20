using Autodoc.CatalogAdmin.Infrastucture.Models.Articles;
using TinyCsvParser.Mapping;

namespace Autodoc.CatalogAdmin.Infrastucture.Services.CsvArticleParser
{
    public class CsvArticleParseModelMapping : CsvMapping<ArticleParseModel>
    {
        public CsvArticleParseModelMapping () : base()
        {
            MapProperty(0, x => x.ArticleId);
        }
    }
}
