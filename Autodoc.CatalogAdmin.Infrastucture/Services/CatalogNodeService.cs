using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Domain.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class CatalogNodeService : ICatalogNodeService
    {
        private readonly IConnectionService _connectionService;

        public CatalogNodeService (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task<CatalogNode> GetCatalogNodeById (int nodeId)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogNode>("seo.spGetCatalogNodeById", new { NodeId = nodeId }, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<CatalogNode>> GetCatalogNodeChildrenById (int nodeId)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogNode>("seo.spGetCatalogNodeChildrenById", new { NodeId = nodeId }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<CatalogNode>> GetCategoryTreeForUpdate()
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogNode>("seo.spGetFullCategoryTreeForUpdate", commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}


