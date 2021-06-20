using AutoMapper;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Application.Common.Mappings
{
    public static class MappingsExtensions
    {
        public static IEnumerable<TDestionation> ProjectTo<TDestionation, TSource> (this IEnumerable<TSource> source, IConfigurationProvider configuration) 
        {
            return configuration.CreateMapper().Map<IEnumerable<TSource>, IEnumerable<TDestionation>>(source);
        }

        public static TDestionation ProjectTo<TDestionation, TSource> (this TSource source, IConfigurationProvider configuration)
        {
            return configuration.CreateMapper().Map<TSource, TDestionation>(source);
        }
    }
}

