using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.CreateNode
{
    public class CreateNodeCommand : INodeCommand, IRequest<int>
    {
        public int ParentId { get; set; }
        public string NameRus { get; set; }
        public string NameUkr { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand, int>
    {
        private readonly ICatalogNodeCommandService _catalogNodeCommandService;

        public CreateNodeCommandHandler(ICatalogNodeCommandService catalogNodeCommandService)
        {
            _catalogNodeCommandService = catalogNodeCommandService;
        }

        public async Task<int> Handle (CreateNodeCommand request, CancellationToken cancellationToken)
        {
            var model = new CatalogNodeDto()
            {
                ParentId = request.ParentId,
                NameRus = request.NameRus,
                NameUkr = request.NameUkr,
                ImageUrl = request.ImageUrl
            };
            var result = await _catalogNodeCommandService.CreateNode(model, cancellationToken);

            return result;
        }
    }     
}
