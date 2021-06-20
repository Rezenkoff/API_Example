using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetCatalogNodeById;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries
{
    public class GetCatalogNodePathQuery : IRequest<IEnumerable<NodeNavPathModel>>, INodeIdQuery
    {
        public int NodeId { get; set; }
    }

    public class GetCatalogNodePathQueryHandler : IRequestHandler<GetCatalogNodePathQuery, IEnumerable<NodeNavPathModel>>
    {
        private readonly IConnectionService _connectionService;
        
        public GetCatalogNodePathQueryHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IEnumerable<NodeNavPathModel>> Handle (GetCatalogNodePathQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var nodePath = (await db.QueryAsync<NodeNavPathModel>("seo.spGetCatalogNodePathById", new { NodeId = request.NodeId }, commandType: CommandType.StoredProcedure));

                return nodePath;
            }
        }
    }
}
