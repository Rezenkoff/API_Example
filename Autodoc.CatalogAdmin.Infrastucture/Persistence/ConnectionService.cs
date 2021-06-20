using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Autodoc.CatalogAdmin.Infrastucture.Persistence
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration _configuration;
        public ConnectionService (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection ()
        {
            var connectionString = _configuration.GetSection("ConnectionStrings:AutodocDbConnection")?.Value;

            return new SqlConnection(connectionString);
        }
    }
}
