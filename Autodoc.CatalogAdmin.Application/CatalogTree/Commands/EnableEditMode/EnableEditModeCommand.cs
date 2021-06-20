using Autodoc.CatalogAdmin.Application.Common.Extensions;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.EnableEditMode
{
    public class EnableEditModeCommand : IRequest
    {

    }

    public class EnablaeEditModeCommandHandler : IRequestHandler<EnableEditModeCommand>
    {                                                                                                
        private readonly IConnectionService _connectionService;

        public EnablaeEditModeCommandHandler(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }                                           

        public async Task<Unit> Handle (EnableEditModeCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {                
                await db.ExecuteAsync("seo.spEnableEditModeCatalog", commandType: CommandType.StoredProcedure);                
            }

            return Unit.Value;
        }
    }
}
