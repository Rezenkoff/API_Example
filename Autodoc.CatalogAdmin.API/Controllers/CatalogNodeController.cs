using Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.CreateNode;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.DeleteNode;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.NodeChildren;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.UpdateNode;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.FindCatalogNode;
using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.API.Controllers
{
    [Authorize(Roles = "Admin, SEO")]
    public class CatalogNodeController : ApiControllerBase
    {
        [HttpGet]
        [Route("get-node-by-id")]
        public async Task<IActionResult> GetNodeById ([FromQuery] int nodeId = 0)
        {
            var nodeWithChildren = await Mediator.Send(new GetCatalogNodeByIdQuery() { NodeId = nodeId });

            return Ok(nodeWithChildren);
        }

        [HttpGet]
        [Route("get-node-path")]
        public async Task<IActionResult> GetNodePath ([FromQuery] int nodeId = 0)
        {
            var nodePath = await Mediator.Send(new GetCatalogNodePathQuery() { NodeId = nodeId });

            return Ok(nodePath);
        }

        [HttpPost]
        [Route("create-node")]
        public async Task<ActionResult<int>> CreateNode (CreateNodeCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("update-node/{nodeId}")]
        public async Task<IActionResult> UpdateNode (int nodeId, UpdateNodeCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }

            if (nodeId != command.NodeId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [Route("node-children-count/{nodeId}")]
        public async Task<ActionResult<int>> GetNodeChildren (int nodeId)
        {
            return await Mediator.Send(new NodeAndChildrenCommand() { NodeId = nodeId });
        }

        [HttpPut]
        [Route("set-delete-node")]
        public async Task<IActionResult> DeleteNode (DeleteNodeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [Route("get-posible-parents-list")]
        public async Task<IActionResult> GetPosibleParentsList ([FromQuery] int nodeId = 0)
        {
            var posibleParents = await Mediator.Send(new GetPosibleParentsListQuery() { NodeId = nodeId });

            return Ok(posibleParents);
        }

        [HttpGet]
        [Route("find-nodes")]
        public async Task<IActionResult> FindNodes([FromQuery] string text)
        {
            var findedNodes = await Mediator.Send(new FindCatalogNodesQuery() { Text = text });

            return Ok(findedNodes);
        }
    }
}
