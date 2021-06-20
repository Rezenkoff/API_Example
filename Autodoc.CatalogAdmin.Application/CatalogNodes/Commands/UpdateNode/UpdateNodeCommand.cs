using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.UpdateNode
{
    public class UpdateNodeCommand : INodeCommand, IRequest
    {
        public int NodeId { get; set; }
        public int ParentId { get; set; }
        public string NameRus { get; set; }
        public string NameUkr { get; set; }
        public string ImageUrl { get; set; }
        public int Vogue { get; set; }
    }

    public class UpdateNodeCommandHandler : IRequestHandler<UpdateNodeCommand>
    {
        private readonly ICatalogNodeCommandService _catalogNodeCommandService;

        public UpdateNodeCommandHandler(ICatalogNodeCommandService catalogNodeCommandService)
        {
            _catalogNodeCommandService = catalogNodeCommandService;
        }

        public async Task<Unit> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            var model = new CatalogNodeDto()
            {
                NodeId = request.NodeId,
                ParentId = request.ParentId,
                NameRus = request.NameRus,
                NameUkr = request.NameUkr,
                ImageUrl = request.ImageUrl,
                Vogue = request.Vogue
            };

            await _catalogNodeCommandService.UpdateNode(model, cancellationToken);
            return Unit.Value;
        }
    }
}
