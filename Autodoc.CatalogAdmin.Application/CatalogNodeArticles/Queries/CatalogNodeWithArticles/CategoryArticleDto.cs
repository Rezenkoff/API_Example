using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;

namespace Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles
{
    public class CategoryArticleDto : IMapFrom<CategoryArticle>
    {
        public int ArticleId { get; set; }
        public int ArticleType { get; set; }
        public string ArticleName { get; set; }
        public bool ArticleIsActive { get; set; }
        public int ArticleSupplierId { get; set; }
        public string CardNumber { get; set; }
        public string ArticleInfo { get; set; }
        public string Measure { get; set; }
        public string ArticleNameUkr { get; set; }

        public void Mapping (Profile profile)
        {
            profile.CreateMap<CategoryArticle, CategoryArticleDto>()                
               .ForMember(d => d.ArticleType, opt => opt.MapFrom(s => (int)s.ArticleType));
        }

    }
}



