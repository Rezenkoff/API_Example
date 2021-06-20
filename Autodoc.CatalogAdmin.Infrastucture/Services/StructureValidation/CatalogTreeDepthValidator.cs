using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services.StructureValidation
{
    public class CatalogTreeDepthValidator : ICatalogValidator
    {
        private IConnectionService _connectionService;

        public CatalogTreeDepthValidator (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<ICatalogValidationResult> Validate ()
        {
            CatalogValidationResult validationResult = new CatalogValidationResult();

            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogValidationNodeDepthModel>("seo.spValidateCatalogTreeDepth", commandType: CommandType.StoredProcedure);

                if (result.Count() == 0)
                {
                    validationResult.Valid = true;
                    return validationResult;
                }

                var nodesDepthGroup = result.GroupBy(i => i.Depth).Select(t => new { Key = t.Key, Items = string.Join($", {System.Environment.NewLine} ", t.Select(i => i.NameRus)) }).Select(g => $"{ g.Key } Уровень - { g.Items }");
                validationResult.Valid = false;
                validationResult.ErrorTitle = "Глубина дерева превышена ";
                validationResult.InvalidNodesNameList = result.Select(p => p.NameRus);
                validationResult.InvalidNodesList = result.Select(i => i.NodeId).ToList();
                validationResult.ErrorDescription = $"{ string.Join($"; {System.Environment.NewLine} ", nodesDepthGroup) }";

                return validationResult;
            }
        }
    }
}
