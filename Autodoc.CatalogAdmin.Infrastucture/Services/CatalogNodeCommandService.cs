using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class CatalogNodeCommandService : ICatalogNodeCommandService
    {
        private readonly IConnectionService _connectionService;

        public CatalogNodeCommandService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task WorkWithModifiedNode(List<CatalogNodeDto> nodes, ActionTypeEnum type, CancellationToken cancellationToken)
        {
            foreach (var node in nodes)
            {
                if (type == ActionTypeEnum.Add)
                {
                    await CreateNode(node, cancellationToken);
                }
                else if (type == ActionTypeEnum.Rename)
                {
                    await UpdateNode(node, cancellationToken);
                }
                else if (type == ActionTypeEnum.Move)
                {
                    await MoveNode(node, cancellationToken);
                }
                else if (type == ActionTypeEnum.Remove)
                {
                    await DeleteNode(node, cancellationToken);
                }
            }
        }

        public async Task<int> CreateNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QuerySingleAsync<int>(new CommandDefinition("seo.spCreateNode",
                    new
                    {
                        ParentId = catalogNode.ParentId,
                        NodeNameRus = catalogNode.NameRus,
                        NodeNameUkr = catalogNode.NameUkr,
                        ImageUrl = catalogNode.ImageUrl
                    },
                    cancellationToken: cancellationToken,
                    commandType: CommandType.StoredProcedure));

                return result;
            }
        }

        public async Task UpdateNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                var result = await db.QueryAsync(new CommandDefinition("seo.spUpdateNode",
                    new
                    {
                        NodeId = catalogNode.NodeId,
                        ParentId = catalogNode.ParentId,
                        NameRus = catalogNode.NameRus,
                        NameUkr = catalogNode.NameUkr,
                        ImageUrl = catalogNode.ImageUrl,
                        Vogue = catalogNode.Vogue
                    },
                    cancellationToken: cancellationToken,
                    commandType: CommandType.StoredProcedure));
            }
        }

        public async Task DeleteNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                await db.QueryAsync(new CommandDefinition("seo.spSetDeleteNode",
                    new
                    {
                        NodeId = catalogNode.NodeId,
                        IsDeleted = Convert.ToInt32(catalogNode.IsDeleted)
                    },
                    cancellationToken: cancellationToken,
                    commandType: CommandType.StoredProcedure));
            }
        }

        public async Task MoveNode(CatalogNodeDto catalogNode, CancellationToken cancellationToken)
        {
            using (var db = _connectionService.GetConnection())
            {
                await db.QueryAsync(new CommandDefinition("seo.spMoveCatalogNode",
                    new
                    {
                        MoveNodeId = catalogNode.NodeId,
                        NewParentId = catalogNode.ParentId
                    },
                    cancellationToken: cancellationToken,
                    commandType: CommandType.StoredProcedure));
            }
        }
    }
}
