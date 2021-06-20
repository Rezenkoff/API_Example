using System;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models
{
    public abstract class CatalogNodeExcelModel
    {
        public virtual int NodeId { get; set; }
    }

    public class CatalogAddNodeExcelModel : CatalogNodeExcelModel
    {
        [Column(1)]
        public string NameRus { get; set; }

        [Column(2)]
        public string NameUkr { get; set; }

        [Column(3)]
        public int ParentId { get; set; }
    }

    public class CatalogRenameNodeExcelModel : CatalogNodeExcelModel
    {
        [Column(1)]
        public override int NodeId { get; set; }

        [Column(3)]
        public string NameRus { get; set; }

        [Column(4)]
        public string NameUkr { get; set; }
    }

    public class CatalogMoveExcelModel : CatalogNodeExcelModel
    {
        [Column(1)]
        public override int NodeId { get; set; }

        [Column(3)]
        public int ParentId { get; set; }
    }

    public class CatalogRemoveExcelModel : CatalogNodeExcelModel
    {
        [Column(1)]
        public override int NodeId { get; set; }
    }



    [AttributeUsage(AttributeTargets.All)]
    public class Column : System.Attribute
    {
        public int ColumnIndex { get; set; }

        public Column(int column)
        {
            ColumnIndex = column;
        }
    }
}
