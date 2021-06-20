using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.ProccessUploadedFile;
using Autodoc.CatalogAdmin.Application.Common.Exceptions;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models.Articles;
using Dapper;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class FileArticleValidator : IFileArticleValidator
    {
        private IConnectionService _connectionService;
        private string ValidationTypeProp = "LOAD_FROM_FILE";

        public FileArticleValidator (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task<IValidateResult> ValidateExisting (IEnumerable<IArticleParseModel> articles)
        {           
            IEnumerable<int> artValidationResult = null;
            var artsToValidate = articles.Select(a => a.ArticleId);
            var validator = new ArticleFromFileValidator();

            List<ValidationResult> validationResult = Enumerable.Empty<ValidationResult>().ToList();
            articles.ToList().ForEach( i => validationResult.Add(validator.Validate(i))); 
                                                                
            using (var db = _connectionService.GetConnection())
            {
                artValidationResult = (await db.QueryAsync<int>("SELECT ART_ART_PP_ID FROM AutodocShared.dbo.ARTICLES WHERE [ART_ART_PP_ID] IN @ArtIds", new { ArtIds = artsToValidate }));
            }

            if (validationResult.Where(v => v.Errors.Count > 0).Any())
            {
                var wrongTypeId = $"Invalid Id for type";
                throw new AutodocValidationException(new[] { new ValidationFailure(ValidationTypeProp, wrongTypeId) });
            }

            var invalidIds = artsToValidate.Except(artValidationResult);
            if (invalidIds.Any())
            {
                var wrongIds = $"Invalid Id`s { string.Join(",", invalidIds)}"; 
                throw new AutodocValidationException( new[] { new ValidationFailure(ValidationTypeProp, wrongIds) });
            }

            return new ValidateResult() { ValidationSuccess = true };
        }
    }
}
