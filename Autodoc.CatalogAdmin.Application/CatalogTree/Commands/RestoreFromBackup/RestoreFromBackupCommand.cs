using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.RestoreFromBackup
{
    public class RestoreFromBackupCommand : IRequest
    {

    }

    public class RestoreFromBackupCommandHandler : IRequestHandler<RestoreFromBackupCommand>
    {
        private readonly IConnectionService _connectionService;

        public RestoreFromBackupCommandHandler (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<Unit> Handle (RestoreFromBackupCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                await db.QueryAsync("seo.spRestoreFromBackupCatalog", commandType: CommandType.StoredProcedure);
            }

            return Unit.Value;
        }
    }
}
