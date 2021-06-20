using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.NodeChildren
{
    public class NodeAndChildrenCommand : IRequest<int>
    {
        public int NodeId { get; set; }
    }

    public class DeleteNodeCommandHandler : IRequestHandler<NodeAndChildrenCommand, int>
    {
        private readonly IConnectionService _connectionService;

        public DeleteNodeCommandHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task<int> Handle (NodeAndChildrenCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var nodesCount = (await db.QueryAsync<int>(new CommandDefinition("SELECT COUNT(*) FROM fnNodeAndChildrenIds(@NodeId)",
                    new
                    {
                        NodeId = request.NodeId
                    },
                    cancellationToken: cancellationToken,
                    commandType: CommandType.StoredProcedure))).FirstOrDefault();


                return nodesCount;
            }
        }
    }
}
