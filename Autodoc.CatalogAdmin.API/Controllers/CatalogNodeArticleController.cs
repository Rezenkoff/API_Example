using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Commands.UploadFileArticles;
using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles;
using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.GetArticlesTypesList;
using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.ProccessUploadedFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.API.Controllers
{
    [Authorize(Roles = "Admin, SEO")]
    public class CatalogNodeArticleController : ApiControllerBase
    {
        [HttpGet]
        [Route("get-node-articles")]
        public async Task<CatalogArticlesResponseVm> GetNodeArticles ([FromQuery] int nodeId, [FromQuery] int typeId)
        {
            return await Mediator.Send(new GetNodeWithArticlesQuery() { NodeId = nodeId, TypeId = typeId });
        }

        [HttpGet]
        [Route("get-articles-types")]
        public async Task<IActionResult> GetArticlesTypes ()
        {
            var response = await Mediator.Send(new GetArticlesTypesListQuery());

            return Ok(response);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload-articles-batch")]
        public async Task<IActionResult> Upload ()
        {
            await Mediator.Send(new UploadFileArticlesCommand() { Files = Request.Form.Files });

            return Ok();
        }

        [HttpGet]
        [Route("proccess-uploaded-file")]
        public async Task<IActionResult> ProccessUploadedFile ([FromQuery] string fileName, [FromQuery] int categoryId)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new ProccessUploadedFileQuery() { FileName = fileName, CategortyId = categoryId });

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
