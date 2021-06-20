using Autodoc.CatalogAdmin.Application.Common.Mappings;
using Autodoc.CatalogAdmin.Application.Common.Models;
using Autodoc.CatalogAdmin.Domain.Entities;
using Autodoc.CatalogAdmin.Domain.Enums;
using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Autodoc.CatalogAdmin.Application.CatalogTree.Commands.UpdateCatalogByExcel.Models
{
    public class ModifiedCatalogNode : IMapFrom<CatalogNode>, IMapFrom<CatalogNodeDto>
    {
        [Required, Range(0, int.MaxValue)]
        public int NodeId { get; set; }

        [Required]
        public int? ParentId { get; set; }

        [Required, MinLength(1)]
        public string NameRus { get; set; }

        [Required, MinLength(1)]
        public string NameUkr { get; set; }

        [Required, Range((int)CatalogLevel.Zero, (int)CatalogLevel.Fourth)]
        public CatalogLevel Level { get; set; } = CatalogLevel.First;

        public string ImageUrl { get; set; }
        public int Vogue { get; set; }
        public bool IsDeleted { get; set; }
        public int ArticlesCount { get; set; }
        public ActionTypeEnum? Modified { get; set; }

        public bool HasArticles => ArticlesCount > 0;


        public ModifiedCatalogNode() { }

        public ModifiedCatalogNode(int parentId, string nameRus, string nameUkr)
        {
            NameRus = nameRus;
            NameUkr = nameUkr;
            ParentId = parentId;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CatalogNode, ModifiedCatalogNode>()
                .ForMember(dest => dest.Modified, opts => opts.MapFrom(src => (ActionTypeEnum?)null));

            profile.CreateMap<ModifiedCatalogNode, CatalogNodeDto>();
        }

        public void RenameNode(string nameRus, string nameUkr)
        {
            NameRus = nameRus;
            NameUkr = nameUkr;
        }

        public void SetNewLevelFromParent(CatalogLevel parentLevel)
        {
            Level = parentLevel + 1;
        }

        public void SetDeleted()
        {
            IsDeleted = true;
        }

        public void SetModified(ActionTypeEnum modifiedType)
        {
            Modified = modifiedType;
        }
    }
}
