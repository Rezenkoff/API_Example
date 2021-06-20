using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Infrastucture.Services
{
    public class ArticleBindService : IArticleBindService
    {
        private IConnectionService _connectionService;
        public ArticleBindService (IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task BindArticlesToCategories (IEnumerable<IArticleParseModel> articlesToBind, int categoryId)
        {
            using (var db = _connectionService.GetConnection())
            {
                foreach (var item in articlesToBind)
                {
                    string processQuery = "INSERT INTO  [AutodocSEO].[dbo].[link_art_tree_auto_part] (part_id, art_id, art_type, link_type, modify_date, ES_ID) VALUES (@categoryId, @articleId, @artType, 0, getdate(), 0)";
                    await db.ExecuteAsync(processQuery, new { articleId = item.ArticleId, artType = item.ArtTypeId, categoryId = categoryId });
                }
            }
        }
    }
}
