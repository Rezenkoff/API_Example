using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Validators;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.CreateNode
{
    public class CreateNodeCommandValidator : NodeCommandBaseValidator<CreateNodeCommand>
    {       
        public CreateNodeCommandValidator (IConnectionService connectionService)  : base(connectionService)
        {
           
        }

        
    }
}
