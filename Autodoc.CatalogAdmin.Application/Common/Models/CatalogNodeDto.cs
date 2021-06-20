using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;

namespace Autodoc.CatalogAdmin.Application.Common.Models
{
    public class CatalogNodeDto : IMapFrom<CatalogNode>
    {
        public int Level { get; set; }
        public string NameRus { get; set; }
        public string NameUkr { get; set; }
        public int NodeId { get; set; }
        public int? ParentId { get; set; }
        public string ImageUrl { get; set; }
        public int Vogue { get; set; }
        public bool IsDeleted { get; set; }
        //public int ArticlesCount { get; set; }

        public void Mapping (Profile profile)
        {
            profile.CreateMap<CatalogNode, CatalogNodeDto>()
               .ForMember(d => d.Level, opt => opt.MapFrom(s => (int)s.Level));
        }
    }
}
