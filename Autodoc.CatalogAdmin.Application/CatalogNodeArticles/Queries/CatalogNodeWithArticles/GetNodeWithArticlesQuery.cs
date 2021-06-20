using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Domain.Entities;
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles
{
    public class GetNodeWithArticlesQuery : IRequest<CatalogArticlesResponseVm>
    {
        public int NodeId { get; set; }
        public int TypeId { get; set; }
    }

    public class GetNodeWithArticlesQueryHandler : IRequestHandler<GetNodeWithArticlesQuery, CatalogArticlesResponseVm>
    {
        private readonly IConnectionService _connectionService;
        private readonly ICatalogNodeArticlesVmBuilder _catalognodeArticlesVmbuilder;

        public GetNodeWithArticlesQueryHandler(IConnectionService connectionService, ICatalogNodeArticlesVmBuilder catalognodeArticlesVmbuilder)
        {
            _connectionService = connectionService;
            _catalognodeArticlesVmbuilder = catalognodeArticlesVmbuilder;
        }

        public async Task<CatalogArticlesResponseVm> Handle (GetNodeWithArticlesQuery request, CancellationToken cancellationToken)
        {  

            using (var db = _connectionService.GetConnection())      
            {
                var result = await db.QueryMultipleAsync("seo.spGetNodeWithArticles", new { 
                    NodeId = request.NodeId, 
                    TypeId = request.TypeId                   
                }, commandType: CommandType.StoredProcedure);

                var arts = result.Read<CategoryArticle>();
                var total = result.Read<int>().First();

                return _catalognodeArticlesVmbuilder.Build(arts, total);
            }
        }
    }
}
