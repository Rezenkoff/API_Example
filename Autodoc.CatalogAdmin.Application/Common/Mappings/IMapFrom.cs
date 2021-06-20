using AutoMapper;

namespace Autodoc.CatalogAdmin.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping (Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
