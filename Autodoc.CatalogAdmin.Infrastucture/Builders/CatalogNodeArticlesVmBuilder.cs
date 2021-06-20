using Autodoc.CatalogAdmin.Application.CatalogNodeArticles.Queries.CatalogNodeWithArticles;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Autodoc.CatalogAdmin.Infrastucture.Builders
{
    public class CatalogNodeArticlesVmBuilder : ICatalogNodeArticlesVmBuilder
    {

        private readonly IMapper _mapper;

        public CatalogNodeArticlesVmBuilder (IMapper mapper)
        {
            _mapper = mapper;
        }

        public CatalogArticlesResponseVm Build (IEnumerable<CategoryArticle> articles, int total)
        {
            var model = new CatalogArticlesResponseVm();
            model.Articles = articles.ProjectTo<CategoryArticleDto, CategoryArticle>(_mapper.ConfigurationProvider);
            model.Total = total;

            return model;
        }
    }
}
