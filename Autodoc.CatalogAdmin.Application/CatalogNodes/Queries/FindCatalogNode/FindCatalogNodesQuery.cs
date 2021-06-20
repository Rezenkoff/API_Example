using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.FindCatalogNode
{
    public class FindCatalogNodesQuery : IRequest<FindCatalogNodeVm>
    {
        public string Text { get; set; }
    }

    public class FindCatalogNodesHandler : IRequestHandler<FindCatalogNodesQuery, FindCatalogNodeVm>
    {
        private readonly IConnectionService _connectionService;

        public FindCatalogNodesHandler(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<FindCatalogNodeVm> Handle(FindCatalogNodesQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<FindCatalogNodeDto>("seo.spFindCatalogNodes", new { Text = request.Text }, commandType: CommandType.StoredProcedure);

                return new FindCatalogNodeVm() { Result = result };
            }
        }
    }
}
