using Autodoc.CatalogAdmin.Application.CatalogTree.Queries.GetCatalogTreeForPreview;
using Autodoc.CatalogAdmin.Application.CatalogTree.Queries.GetCatalogTreeState;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.API.Controllers
{
    [Authorize(Roles = "Admin, SEO")]
    public class CatalogTreeController : ApiControllerBase
    {
        [HttpGet]
        [Route("catalog-tree-state")]
        public async Task<IActionResult> GetCatalogTreeState ()
        {
            var result = await Mediator.Send(new GetCatalogTreeSateQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("get-category-tree-for-preview")]
        public async Task<IActionResult> GetCategoryTreeForPreview()
        {
            var result = await Mediator.Send(new GetCatalogTreeForPreviewQuery());

            return Ok(result);
        }
    }
}
