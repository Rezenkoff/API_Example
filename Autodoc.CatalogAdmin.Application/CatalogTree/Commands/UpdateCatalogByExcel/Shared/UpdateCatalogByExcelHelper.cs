using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models;
using Autodoc.CatalogAdmin.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Shared
{
    public static class UpdateCatalogByExcelHelper
    {
        public static string UpdateCategoryTree(List<ModifiedCatalogNode> catalog, CatalogAddNodeExcelModel addNodeModel)
        {
            var foundNode = catalog.Find(x => x.NameRus == addNodeModel.NameRus);
            if (foundNode != null)
            {
                return UpdateCatalogByExcelErrors.CatalogContainsNodeWithName(addNodeModel.NameRus);
            }

            var parent = catalog.Find(x => x.NodeId == addNodeModel.ParentId);
            if (parent is null)
            {
                return UpdateCatalogByExcelErrors.ParentNotExist(addNodeModel.NameRus);
            }
            else if (parent.Level == CatalogLevel.Fourth)
            {
                return UpdateCatalogByExcelErrors.ParentMustNotHaveFourthLevel(addNodeModel.NodeId);
            }

            var newNode = new ModifiedCatalogNode(addNodeModel.ParentId, addNodeModel.NameRus, addNodeModel.NameUkr);
            newNode.SetNewLevelFromParent(parent.Level);
            newNode.SetModified(ActionTypeEnum.Add);

            var (isValidNode, checkNodeError) = CheckNode(newNode);
            if (!isValidNode)
            {
                return checkNodeError;
            }

            catalog.Add(newNode);
            return string.Empty;
        }


        public static string UpdateCategoryTree(List<ModifiedCatalogNode> catalog, CatalogRenameNodeExcelModel renameNodeModel)
        {
            var modifiedNode = catalog.Find(x => x.NodeId == renameNodeModel.NodeId);
            if (modifiedNode is null)
            {
                return UpdateCatalogByExcelErrors.NodeNotExists(modifiedNode.NodeId);
            }

            modifiedNode.RenameNode(renameNodeModel.NameRus, renameNodeModel.NameUkr);
            modifiedNode.SetModified(ActionTypeEnum.Rename);

            var (isValidNode, checkNodeError) = CheckNode(modifiedNode);
            if (!isValidNode)
            {
                return checkNodeError;
            }

            return string.Empty;
        }


        public static string UpdateCategoryTree(List<ModifiedCatalogNode> catalog, CatalogMoveExcelModel moveNodeModel)
        {
            var modifiedNode = catalog.Find(x => x.NodeId == moveNodeModel.NodeId);
            if (modifiedNode is null)
            {
                return UpdateCatalogByExcelErrors.NodeNotExists(modifiedNode.NodeId);
            }

            var parentNode = catalog.Find(x => x.NodeId == modifiedNode.ParentId);
            if (parentNode is null)
            {
                return UpdateCatalogByExcelErrors.ParentNotExist(modifiedNode.NodeId);
            }

            modifiedNode.ParentId = moveNodeModel.ParentId;
            modifiedNode.SetNewLevelFromParent(parentNode.Level);
            modifiedNode.SetModified(ActionTypeEnum.Move);

            return string.Empty;
        }


        public static string UpdateCategoryTree(List<ModifiedCatalogNode> catalog, CatalogRemoveExcelModel removeNodeModel)
        {
            var nodeForRemove = catalog.Find(x => x.NodeId == removeNodeModel.NodeId);
            if (nodeForRemove is null)
            {
                return UpdateCatalogByExcelErrors.NodeNotExists(removeNodeModel.NodeId);
            }
            else if (nodeForRemove.HasArticles)
            {
                return UpdateCatalogByExcelErrors.NodeMustNotHaveArticles(nodeForRemove.NodeId);
            }

            var hasChildrenNodes = catalog.Any(x => x.ParentId == nodeForRemove.NodeId);
            if (hasChildrenNodes)
            {
                return UpdateCatalogByExcelErrors.NodeMustNotHaveChilded(nodeForRemove.NodeId);
            }

            nodeForRemove.SetDeleted();
            nodeForRemove.SetModified(ActionTypeEnum.Remove);

            return string.Empty;
        }


        private static (bool, string) CheckNode(ModifiedCatalogNode checkNode)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(checkNode);
            if (!Validator.TryValidateObject(checkNode, context, results, true))
            {
                return (false, $"{results.FirstOrDefault().ErrorMessage} Node: {checkNode.NameRus}");
            }
            return (true, string.Empty);
        }
    }

    public static class UpdateCatalogByExcelErrors
    {
        public static string NodeNotExists(int nodeId) => $"Catalog does not contain node with id: {nodeId}";

        public static string ParentNotExist(int nodeId) => $"Parent not exists for node with id: {nodeId}";
        public static string ParentNotExist(string nodeName) => $"Parent not exists for node with id: {nodeName}";
        public static string ParentMustNotHaveFourthLevel(int nodeId) => $"Parent must not be fourth level for node with id: {nodeId}";

        public static string NodeMustNotHaveChilded(int nodeId) => $"Node with id: {nodeId} must not have a child nodes";

        public static string NodeMustNotHaveArticles(int nodeId) => $"Node with id: {nodeId} must not have a articles";

        public static string CatalogContainsNodeWithName(string nodeName) => $"Catalog already contains node with name: {nodeName}";
    }
}
