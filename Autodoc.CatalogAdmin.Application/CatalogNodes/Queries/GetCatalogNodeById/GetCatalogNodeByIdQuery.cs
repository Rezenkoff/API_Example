using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetCatalogNodeById;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Domain.Entities;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries
{
    public class GetCatalogNodeByIdQuery : IRequest<CatalogNodeVm>, INodeIdQuery
    {
        public int NodeId { get; set; }
    }

    public class GetCatalogNodeByIdQueryHandler :  IRequestHandler<GetCatalogNodeByIdQuery, CatalogNodeVm>
    {
        private readonly IConnectionService _connectionService;
        private readonly ICatalogNodeVmBuilder _catalogNodeBuilder;
        private readonly ICatalogNodeService _catalogNodeService;
        public GetCatalogNodeByIdQueryHandler(IConnectionService connectionService, ICatalogNodeVmBuilder catalogNodeBuilder, ICatalogNodeService catalogNodeService)
        {
            _connectionService = connectionService;
            _catalogNodeBuilder = catalogNodeBuilder;
            _catalogNodeService = catalogNodeService;
        }

        public async Task<CatalogNodeVm> Handle(GetCatalogNodeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var node = await _catalogNodeService.GetCatalogNodeById(request.NodeId); 
                var children = await _catalogNodeService.GetCatalogNodeChildrenById(request.NodeId);

                return _catalogNodeBuilder.Build(node, children);
            }
        }
    }
}
