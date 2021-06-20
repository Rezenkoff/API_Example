using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models.Articles;
using Autodoc.CatalogAdmin.Infrastucture.Services.CsvArticleParser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class CsvFileArticleParser : IFileArticleParser
    {
        public IEnumerable<IArticleParseModel> ParseFile (string fileName)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ';');
            var csvParser = new CsvParser<ArticleParseModel>(csvParserOptions, new CsvArticleParseModelMapping());

            var filePath = Path.Combine("Resources", "Articles", fileName);
            var records = csvParser.ReadFromFile(filePath, Encoding.UTF8);

            return records.Select(x => x.Result).ToList();
        }
    }
}
