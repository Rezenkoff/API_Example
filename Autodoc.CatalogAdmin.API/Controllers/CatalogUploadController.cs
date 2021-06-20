using Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.UpdateCatalogByExcel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.API.Controllers
{
    public class CatalogUploadController : ApiControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateCatalogFromExcel()
        {
            if ((bool)!Request?.Form?.Files?.Any())
            {
                return BadRequest();
            }

            IFormFile file = Request.Form.Files.First();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var result = await Mediator.Send(new UpdateCatalogByExcelCommand() { FileStream = ms });
                return Ok(result);
            }
        }
    }
}
