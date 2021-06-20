using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class ArticleFileProccessor : IArticleFileProccessor
    {
        private IFileArticleParser _fileArticleParser;
        private IFileArticleValidator _fileArticleValidator;
        private IArticleBindService _articleBindService;

        public ArticleFileProccessor (IFileArticleParser fileArticleParser, IFileArticleValidator fileArticleValidator, IArticleBindService articleBindService)
        {
            _fileArticleParser = fileArticleParser;
            _fileArticleValidator = fileArticleValidator;
            _articleBindService = articleBindService;
        }
        public async Task<bool> ProccessFile (string fileName, int categoryId)
        {
            var articlesFromFile = _fileArticleParser.ParseFile(fileName);
            var result = await _fileArticleValidator.ValidateExisting(articlesFromFile);

            if(result.ValidationSuccess)
            {
                await _articleBindService.BindArticlesToCategories(articlesFromFile, categoryId);
            }

            return true;
        }
    }
}
