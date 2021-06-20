using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Domain.Entities;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents
{
    public class GetPosibleParentsListQuery : IRequest<IEnumerable<ParentNodeGroupVm>>, INodeIdQuery
    {
        public int NodeId { get; set; }
    }

    public class GetPosibleParentsListQueryHandler : IRequestHandler<GetPosibleParentsListQuery, IEnumerable<ParentNodeGroupVm>>
    {
        private readonly IConnectionService _connectionService;
        private readonly ICatalogParentKeyValueVmBuilder _catalogParentKeyValueBuilder;
        public GetPosibleParentsListQueryHandler (IConnectionService connectionService, ICatalogParentKeyValueVmBuilder catalogParentKeyValueBuilder)
        {
            _connectionService = connectionService;
            _catalogParentKeyValueBuilder = catalogParentKeyValueBuilder;
        }

        public async Task<IEnumerable<ParentNodeGroupVm>> Handle (GetPosibleParentsListQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<CatalogNode>("seo.spGetPosibleParentsList", new {  NodeId = request.NodeId }  , commandType: CommandType.StoredProcedure);

                return _catalogParentKeyValueBuilder.Build(result);
            }
        }
    }
}
