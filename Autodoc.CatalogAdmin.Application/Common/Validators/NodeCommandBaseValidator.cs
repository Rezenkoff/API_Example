using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Dapper;
using FluentValidation;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Validators
{
    public class NodeCommandBaseValidator<TCommand> : AbstractValidator<TCommand> where TCommand : INodeCommand
    {
        protected readonly IConnectionService _connectionService;
        public NodeCommandBaseValidator (IConnectionService connectionService)
        {
            _connectionService = connectionService;

            RuleFor(n => n.NameRus)
                .MaximumLength(200).WithMessage("NameRus must not exceed 200 characters.")
                .NotEmpty().WithMessage("NameRus is empty")
                .NotNull().WithMessage("NameRus is NULL")
                .MustAsync(BeUniqueNodeName).WithMessage("NameRus should be unique");

            RuleFor(n => n.NameUkr).MaximumLength(200)
                .WithMessage("NameUkr must not exceed 200 characters.")                 
                .MustAsync(BeUniqueNodeName).WithMessage("NameUkr should be unique");

            RuleFor(n => n.ImageUrl)
                .Matches(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?")
                .WithMessage("ImageUrl is not a valid URL");

            RuleFor(n => n.ParentId)
                .GreaterThanOrEqualTo(0).WithMessage("ParentId can't be lower than 0")
                .MustAsync(ShouldExistParentId).WithMessage("Unexisting ParentId");
        }

        public virtual object GetParameters (TCommand model, string NodeName) { return new { NodeName = NodeName }; }

        public async Task<bool> BeUniqueNodeName (TCommand model, string NodeName, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                return !(await db.QueryAsync<bool>("seo.spCheckNodeNameUniqnes", GetParameters(model, NodeName), commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
        }

        public async Task<bool> ShouldExistParentId (TCommand model, int ParentId, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                return (await db.QueryAsync<bool>("seo.spCheckParentIdExists", new { ParentId = ParentId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
        }
    }
}
