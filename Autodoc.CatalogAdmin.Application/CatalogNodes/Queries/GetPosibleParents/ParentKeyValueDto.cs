using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Queries.GetPosibleParents
{
    public class ParentKeyValueDto : IMapFrom<CatalogNode>
    {
        public int Id { get; set; }
        public string Value { get; set; } = "";
        public int Level { get; set; } = 0;

        public void Mapping (Profile profile)
        {
            profile.CreateMap<CatalogNode, ParentKeyValueDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.NodeId))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.NameRus))
                .ForMember(d => d.Level, opt => opt.MapFrom(s => (int)s.Level));
        }
    }
}
