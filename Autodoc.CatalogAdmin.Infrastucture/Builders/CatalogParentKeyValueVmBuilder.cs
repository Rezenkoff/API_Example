using Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Infrastucture.Builders
{
    public class CatalogParentKeyValueVmBuilder : ICatalogParentKeyValueVmBuilder
    {
        private readonly IMapper _mapper;

        public CatalogParentKeyValueVmBuilder (IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<ParentNodeGroupVm> Build (IEnumerable<CatalogNode> nodesList)
        {
            var group = nodesList.ProjectTo<ParentKeyValueDto, CatalogNode>(_mapper.ConfigurationProvider);

            return group.GroupBy(g => g.Level).Select(gr =>
            new ParentNodeGroupVm()
            {
                GroupName = $"Level {gr.Key}",
                GroupedParentNodes = gr.Select(sg =>
                new NodeKeyValueDto()
                {
                    Id = sg.Id,
                    Value = sg.Value
                })
            });
        }
    }
}

