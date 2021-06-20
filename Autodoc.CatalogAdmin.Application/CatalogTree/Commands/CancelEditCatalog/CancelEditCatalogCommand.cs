using Autodoc.CatalogAdmin.Application.Common.Extensions;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.CancelEditCatalog
{
    public class CancelEditCatalogCommand : IRequest
    {

    }

    public class CancelEditCatalogCommandHandler : IRequestHandler<CancelEditCatalogCommand>
    {
        private readonly IConnectionService _connectionService;

        public CancelEditCatalogCommandHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<Unit> Handle (CancelEditCatalogCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                await db.QueryAsync("seo.spCancelEditCatalog", commandType: CommandType.StoredProcedure);
            }

            return Unit.Value;
        }
    }
}
