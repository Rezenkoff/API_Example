using System.Data;

namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface IConnectionService
    {
        IDbConnection GetConnection ();
    }
}
