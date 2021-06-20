using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services.StructureValidation
{
    public class DeletedNodesHasChildrenValidator : ICatalogValidator
    {
        private IConnectionService _connectionService;

        public DeletedNodesHasChildrenValidator (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<ICatalogValidationResult> Validate ()
        {
            CatalogValidationResult validationResult = new CatalogValidationResult();

            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogDeletedNodesWithChildren>("seo.spValidateDeletedNodesHasChildren", commandType: CommandType.StoredProcedure);

                if (result.Count() == 0)
                {
                    validationResult.Valid = true;
                    return validationResult;
                }

                var nodesValidationResult = result.Select(t => $"{ t.NodeId } - { t.NameRus }");
                validationResult.Valid = false;
                validationResult.ErrorTitle = "Удаляемые категории содержат подкатегории";
                validationResult.InvalidNodesList = result.Select(t => t.NodeId);
                validationResult.InvalidNodesNameList = result.Select(t => t.NameRus);
                validationResult.ErrorDescription = $"{string.Join(",", nodesValidationResult)}";

                return validationResult;
            }
        }
    }
}
