using Autodoc.CatalogAdmin.Application.Common.Extensions;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.ApplyNewCatalog
{
    public class ApplyNewCatalogCommand : IRequest
    {

    }

    public class ApplyNewCatalogCommandHandler : IRequestHandler<ApplyNewCatalogCommand>
    {
        private readonly IConnectionService _connectionService;

        public ApplyNewCatalogCommandHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<Unit> Handle (ApplyNewCatalogCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            { 
                await db.QueryAsync("seo.spApplyNewCatalog", commandType: CommandType.StoredProcedure);                
            }

            return Unit.Value;
        }
    }
}
