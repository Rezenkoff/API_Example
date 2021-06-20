using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Commands.UpdateArticle
{
    public class UpdateArticleCommand : IRequest
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public bool ArticleIsActive { get; set; }
        public string ArticleInfo { get; set; }
        public string Measure { get; set; }
        public string ArticleNameUkr { get; set; }
    }

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IConnectionService _connectionService;

        public UpdateArticleCommandHandler (IConnectionService connectionService) { _connectionService = connectionService; }
        public async Task<Unit> Handle (UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync("seo.spUpdateArticle",
                new
                {
                    ArticleId = request.ArticleId,
                    ArticleName = request.ArticleName,
                    ArticleIsActive = request.ArticleIsActive,
                    ArticleInfo = request.ArticleInfo,
                    Measure = request.Measure,
                    ArticleNameUkr = request.ArticleNameUkr
                }, commandType: CommandType.StoredProcedure);
            }

            return Unit.Value;
        }
    }
}

