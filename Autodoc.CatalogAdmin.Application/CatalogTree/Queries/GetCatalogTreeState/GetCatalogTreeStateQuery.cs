using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Queries.GetCatalogTreeState
{
    public class GetCatalogTreeSateQuery : IRequest<CatalogTreeStateVm>
    {

    }

    public class GetCatalogTreeStateQueryHandler : IRequestHandler<GetCatalogTreeSateQuery, CatalogTreeStateVm>
    {
        private readonly IConnectionService _connectionService;
        public GetCatalogTreeStateQueryHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<CatalogTreeStateVm> Handle (GetCatalogTreeSateQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = (await db.QueryAsync<CatalogTreeStateVm>("seo.spGetCatalogTreeState", commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
        }
    }
}
