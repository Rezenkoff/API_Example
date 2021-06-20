using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.ApplyNewCatalog;
using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.CancelEditCatalog;
using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.EnableEditMode;
using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.RestoreFromBackup;
using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.ValidateCatalogStructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.API.Controllers
{
    [Authorize(Roles = "Admin, SEO")]
    public class StructureManipulationController : ApiControllerBase 
    {        
        [HttpGet]
        [Route("enable-edit-mode")]
        public async Task<IActionResult> EnableEditMode()
        {
            await Mediator.Send(new EnableEditModeCommand());

            return NoContent();
        }

        [HttpGet]
        [Route("validate-catalog")]
        public async Task<IActionResult> ValidateCatalogStructure()
        {
            var result = await Mediator.Send(new ValidateCatalogStructureCommand());

            return Ok(result);
        }

        [HttpGet]
        [Route("cancel-edit-catalog")]
        public async Task<IActionResult> CancelEditCatalog ()
        {
            await Mediator.Send(new CancelEditCatalogCommand());

            return NoContent();
        }

        [HttpGet]
        [Route("restore-from-backup-catalog")]
        public async Task<IActionResult> RestoreFromBackupCatalog ()
        {
            await Mediator.Send(new RestoreFromBackupCommand());

            return NoContent();
        }

        [HttpGet]
        [Route("apply-new-catalog")]
        public async Task<IActionResult> ApplyNewCatalog()
        {
            await Mediator.Send(new ApplyNewCatalogCommand());

            return NoContent();
        }
    }
}
