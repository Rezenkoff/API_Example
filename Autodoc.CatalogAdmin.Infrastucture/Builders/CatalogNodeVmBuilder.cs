using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetCatalogNodeById;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Autodoc.CatalogAdmin.Infrastucture.Builders
{
    public class CatalogNodeVmBuilder : ICatalogNodeVmBuilder
    {
        private readonly IMapper _mapper;

        public CatalogNodeVmBuilder (IMapper mapper)
        {
            _mapper = mapper;
        }

        public CatalogNodeVm Build (CatalogNode node, IEnumerable<CatalogNode> nodeChildren)
        {
            var model = new CatalogNodeVm();
            
            model.ParentNode = node.ProjectTo<CatalogNodeDto, CatalogNode>(_mapper.ConfigurationProvider);
            model.NodeChildren = nodeChildren.ProjectTo<CatalogNodeDto, CatalogNode>(_mapper.ConfigurationProvider);

            return model;
        }
    }
}
