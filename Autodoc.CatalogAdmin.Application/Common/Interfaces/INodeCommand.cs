namespace Autodoc.CatalogAdmin.Application.Common.Interfaces
{
    public interface INodeCommand
    {
        int ParentId { get; set; }
        string NameRus { get; set; }
        string NameUkr { get; set; }
        string ImageUrl { get; set; }
    }
}
