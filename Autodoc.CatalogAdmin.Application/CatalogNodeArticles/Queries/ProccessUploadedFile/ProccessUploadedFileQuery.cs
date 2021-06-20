using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.ProccessUploadedFile
{
    public class ProccessUploadedFileQuery : IRequest<bool>
    {
        public string FileName { get; set; }
        public int CategortyId { get; set; }
    }

    public class ProccessUploadedFileQueryHandler : IRequestHandler<ProccessUploadedFileQuery, bool>
    {
        private IArticleFileProccessor _articleFileProccessor;

        public ProccessUploadedFileQueryHandler (IArticleFileProccessor articleFileProccessor)
        {
            _articleFileProccessor = articleFileProccessor;
        }

        public async Task<bool> Handle (ProccessUploadedFileQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleFileProccessor.ProccessFile(request.FileName, request.CategortyId);

            return result;
        }
    }
}
