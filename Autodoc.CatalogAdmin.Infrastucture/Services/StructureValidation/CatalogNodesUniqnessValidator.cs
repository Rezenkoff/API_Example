using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Autodoc.CatalogAdmin.Infrastucture.Services.StructureValidation
{
    public class CatalogNodesUniqnessValidator : ICatalogValidator
    {
        private IConnectionService _connectionService;
        public CatalogNodesUniqnessValidator (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task<ICatalogValidationResult> Validate ()
        {
            CatalogValidationResult validationResult = new CatalogValidationResult();

            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogUniqValidationModel>("seo.spValidateCatalogNodesUniqness", commandType: CommandType.StoredProcedure);

                if (result.Count() == 0)
                {
                    validationResult.Valid = true;
                    return validationResult;
                }

                var nodesValidationResult = result.Select( t => $"{ t.NameRus } - { t.Repeats }");
                validationResult.Valid = false;
                validationResult.ErrorTitle = "В дереве есть не уникальные категории";
                validationResult.InvalidNodesNameList = result.Select(t => t.NameRus);
                validationResult.ErrorDescription = $"{string.Join(",", nodesValidationResult)}" ;

                return validationResult;
            }
        }
    }
}
