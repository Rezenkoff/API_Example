using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.DeleteNode
{
    public class DeleteNodeCommand : IRequest
    {
        public int NodeId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand>
    {
        private readonly ICatalogNodeCommandService _catalogNodeCommandService;

        public DeleteNodeCommandHandler(ICatalogNodeCommandService catalogNodeCommandService)
        {
            _catalogNodeCommandService = catalogNodeCommandService;
        }

        public async Task<Unit> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var model = new CatalogNodeDto()
            {
                NodeId = request.NodeId,
                IsDeleted = request.IsDeleted
            };

            await _catalogNodeCommandService.DeleteNode(model, cancellationToken);
            return Unit.Value;
        }
    }
}
