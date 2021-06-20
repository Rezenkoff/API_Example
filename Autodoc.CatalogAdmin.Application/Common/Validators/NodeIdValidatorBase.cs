using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using FluentValidation;

namespace Autodoc.CatalogAdmin.Application.Common.Validators
{
    public class NodeIdValidatorBase<TCommand> : AbstractValidator<TCommand> where TCommand : INodeIdQuery
    {
        public NodeIdValidatorBase ()
        {
            RuleFor(n => n.NodeId).GreaterThanOrEqualTo(0).WithMessage("NodeId should be greate than 0");
        }
    }
}
