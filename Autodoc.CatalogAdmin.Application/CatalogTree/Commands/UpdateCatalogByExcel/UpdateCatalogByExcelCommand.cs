using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models;
using Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Shared;
using Autodoc.CatalogAdmin.Application.Common.Interfaces;
using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Domain.Entities;
using AutoMapper;
using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autodoc.CatalogAdmin.Application.CatalogNodes.Commands.UpdateCatalogByExcel
{
    public class UpdateCatalogByExcelCommand : IRequest<Result>
    {
        public MemoryStream FileStream { get; set; }
    }

    public class UpdateNodesByExcelCommandHandler : IRequestHandler<UpdateCatalogByExcelCommand, Result>
    {
        private readonly ICatalogNodeService _catalogNodeService;
        private readonly ICatalogNodeCommandService _catalogNodeCommandService;

        private readonly IMapper _mapper;

        public UpdateNodesByExcelCommandHandler(ICatalogNodeService catalogNodeService, ICatalogNodeCommandService catalogNodeCommandService, IMapper mapper)
        {
            _catalogNodeService = catalogNodeService;
            _catalogNodeCommandService = catalogNodeCommandService;

            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateCatalogByExcelCommand request, CancellationToken cancellationToken)
        {
            var nodes = await _catalogNodeService.GetCategoryTreeForUpdate();
            var currentNodes = nodes.ProjectTo<ModifiedCatalogNode, CatalogNode>(_mapper.ConfigurationProvider);

            var modifiedNodes = new List<ModifiedCatalogNode>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage(request.FileStream))
            {
                var expectedWorksheets = Enum.GetValues(typeof(ActionTypeEnum));
                if (pck.Workbook.Worksheets.Count != expectedWorksheets.Length)
                {
                    return Result.Failure(new List<string>() { $"Excel file must have {expectedWorksheets.Length} worksheet" });
                }

                foreach (ActionTypeEnum type in expectedWorksheets)
                {
                    var worksheet = pck.Workbook.Worksheets[(int)type];
                    var (isDataGetted, worksheetData) = GetDataFromWorksheet(type, worksheet);
                    if (!isDataGetted)
                    {
                        return Result.Failure(GetErrorsWithWorksheetName(type));
                    }

                    var (updateResult, modNodes) = UpdateTree(currentNodes.ToList(), worksheetData, type);
                    if (!updateResult.Succeeded)
                    {
                        updateResult.Errors = GetErrorsWithWorksheetName(type, updateResult.Errors).ToArray();
                        return updateResult;
                    }

                    modifiedNodes.AddRange(modNodes);
                }

                foreach (ActionTypeEnum type in Enum.GetValues(typeof(ActionTypeEnum)))
                {
                    var typeNodes = modifiedNodes.Where(x => x.Modified == type);

                    var dtoNodes = typeNodes.ProjectTo<CatalogNodeDto, ModifiedCatalogNode>(_mapper.ConfigurationProvider);
                    await _catalogNodeCommandService.WorkWithModifiedNode(dtoNodes.ToList(), type, cancellationToken);
                }
            }

            return (Result.Success());
        }



        private static (bool, IEnumerable<CatalogNodeExcelModel>) GetDataFromWorksheet(ActionTypeEnum type, ExcelWorksheet worksheet)
        {
            try
            {
                return type switch
                {
                    ActionTypeEnum.Add => (true, worksheet.ConvertSheetToObjects<CatalogAddNodeExcelModel>().ToList()),
                    ActionTypeEnum.Rename => (true, worksheet.ConvertSheetToObjects<CatalogRenameNodeExcelModel>().ToList()),
                    ActionTypeEnum.Move => (true, worksheet.ConvertSheetToObjects<CatalogMoveExcelModel>().ToList()),
                    ActionTypeEnum.Remove => (true, worksheet.ConvertSheetToObjects<CatalogRemoveExcelModel>().ToList()),
                    _ => (false, null)
                };
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        private static (Result, List<ModifiedCatalogNode>) UpdateTree(List<ModifiedCatalogNode> currentNodes, IEnumerable<CatalogNodeExcelModel> data, ActionTypeEnum type)
        {
            var errors = data.Select(nodeModel =>
            {
                return type switch
                {
                    ActionTypeEnum.Add => UpdateCatalogByExcelHelper.UpdateCategoryTree(currentNodes, nodeModel as CatalogAddNodeExcelModel),
                    ActionTypeEnum.Rename => UpdateCatalogByExcelHelper.UpdateCategoryTree(currentNodes, nodeModel as CatalogRenameNodeExcelModel),
                    ActionTypeEnum.Move => UpdateCatalogByExcelHelper.UpdateCategoryTree(currentNodes, nodeModel as CatalogMoveExcelModel),
                    ActionTypeEnum.Remove => UpdateCatalogByExcelHelper.UpdateCategoryTree(currentNodes, nodeModel as CatalogRemoveExcelModel),
                    _ => "Type not found"
                };
            }).Where(x => !string.IsNullOrEmpty(x)).ToList();

            if (errors.Any())
            {
                return (Result.Failure(errors), currentNodes);
            }

            return (Result.Success(), currentNodes.Where(x => x.Modified == type).ToList());
        }

        private static IEnumerable<string> GetErrorsWithWorksheetName(ActionTypeEnum type, IEnumerable<string> errors = null)
        {
            var newErrors = errors?.ToList() ?? new List<string> { "Something went wrong when parse excel" };
            newErrors.Insert(0, $"{type} worksheet");
            return newErrors;
        }
    }
}
