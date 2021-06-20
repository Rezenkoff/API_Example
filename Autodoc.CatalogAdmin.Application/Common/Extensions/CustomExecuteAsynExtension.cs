using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.Common.Extensions
{
    public static class CustomExecuteAsynExtension
    {
        public static async Task<string> ExecuteSpAsync (this IDbConnection connection, string procedureName)
        {
            var returnResultName = "@Result";
            var parameters = new DynamicParameters();
            parameters.Add(name: returnResultName, dbType: DbType.String, direction: ParameterDirection.ReturnValue);

            await connection.ExecuteAsync(procedureName, param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>(returnResultName);
        }
    }
}
