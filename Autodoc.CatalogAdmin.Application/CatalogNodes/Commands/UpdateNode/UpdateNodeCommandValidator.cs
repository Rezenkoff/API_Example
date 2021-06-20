using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Validators;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.UpdateNode
{
    public class UpdateNodeCommandValidator : NodeCommandBaseValidator<UpdateNodeCommand>
    {
        public UpdateNodeCommandValidator (IConnectionService connectionService) : base(connectionService)
        {

        }

        public override object GetParameters (UpdateNodeCommand model, string NodeName)
        {
            return new { NodeName = NodeName, NodeId = model.NodeId };
        }
    }
}
