using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Commands.UploadFileArticles
{
    public class UploadFileArticlesCommand : IRequest
    {
        public IFormFileCollection Files { get; set; }
    }
    public class UploadFileArticlesCommandHandler : IRequestHandler<UploadFileArticlesCommand>
    {
        public UploadFileArticlesCommandHandler ()
        {
            
        }

        public async Task<Unit> Handle (UploadFileArticlesCommand request, CancellationToken cancellationToken)
        {
            var file = request.Files[0];

            var folderName = Path.Combine("Resources", "Articles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
