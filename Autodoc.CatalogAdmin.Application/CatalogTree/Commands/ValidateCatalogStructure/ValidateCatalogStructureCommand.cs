using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.ValidateCatalogStructure
{
    public class ValidateCatalogStructureCommand : IRequest<ICatalogValidationResult>
    {

    }

    public class ValidateCatalogStructureHandler : IRequestHandler<ValidateCatalogStructureCommand, ICatalogValidationResult>
    {
        private readonly IValidationProccessor _validationProccesor;
        public ValidateCatalogStructureHandler (IValidationProccessor validationProccesor)
        {
            _validationProccesor = validationProccesor;
        }

        public async Task<ICatalogValidationResult> Handle (ValidateCatalogStructureCommand request, CancellationToken cancellationToken)
        {
            return await _validationProccesor.StartValidation();
        }

    }
}
