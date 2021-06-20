using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Infrastucture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services.StructureValidation
{
    public class ValidationProccessor : IValidationProccessor
    {
        private readonly IEnumerable<ICatalogValidator> _validators;

        public ValidationProccessor (IConnectionService connectionService)
        {
            _validators = new List<ICatalogValidator>()
            {
                new CatalogTreeDepthValidator(connectionService),
                new CatalogNodesUniqnessValidator(connectionService),
                new DeletedNodesHasChildrenValidator(connectionService),
                new DeletedNodesWithArticlesValidator(connectionService)                
            };
        }
        public async Task<ICatalogValidationResult> StartValidation ()
        {
            ICatalogValidationResult result = new CatalogValidationResult();
            foreach (var validator in _validators)
            {
                result = await validator.Validate();

                if (!result.Valid)
                {
                    break;
                }
            }

            return result;
        }
    }
}
