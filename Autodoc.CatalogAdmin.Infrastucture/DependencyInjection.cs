using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Infrastucture.Persistence;
using Autodoc.CatalogAdmin.Infrastucture.Builders;
using Autodoc.CatalogAdmin.Infrastucture.Services;
using Autodoc.CatalogAdmin.Infrastucture.Services.StructureValidation;

namespace Autodoc.CatalogAdmin.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddTransient<ICatalogNodeService, CatalogNodeService>();
            services.AddTransient<ICatalogNodeVmBuilder, CatalogNodeVmBuilder>();
            services.AddTransient<ICatalogParentKeyValueVmBuilder, CatalogParentKeyValueVmBuilder>();
            services.AddTransient<ICatalogNodeArticlesVmBuilder, CatalogNodeArticlesVmBuilder>();
            services.AddTransient<IArticleFileProccessor, ArticleFileProccessor>();
            services.AddTransient<IFileArticleParser, CsvFileArticleParser>();
            services.AddTransient<IFileArticleValidator, FileArticleValidator>();
            services.AddTransient<IArticleBindService, ArticleBindService>();
            services.AddTransient<IValidationProccessor, ValidationProccessor>();
            services.AddTransient<ICatalogNodeCommandService, CatalogNodeCommandService>();

            return services;
        }
    }
}
