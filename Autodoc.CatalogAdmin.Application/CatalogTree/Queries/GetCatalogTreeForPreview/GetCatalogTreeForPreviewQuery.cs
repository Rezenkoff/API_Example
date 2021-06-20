using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Queries.GetCatalogTreeForPreview
{
    public class GetCatalogTreeForPreviewQuery : IRequest<GetCatalogTreeForPreviewVm>
    {
    }

    public class GetCategoryTreeForPreviewHandler : IRequestHandler<GetCatalogTreeForPreviewQuery, GetCatalogTreeForPreviewVm>
    {
        private readonly IConnectionService _connectionService;

        public GetCategoryTreeForPreviewHandler(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<GetCatalogTreeForPreviewVm> Handle(GetCatalogTreeForPreviewQuery request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync<GetCatalogTreeForPreviewDto>("seo.spGetCategoryTreeForPreview", commandType: CommandType.StoredProcedure);

                return new GetCatalogTreeForPreviewVm() { Result = result };
            }
        }
    }
}
