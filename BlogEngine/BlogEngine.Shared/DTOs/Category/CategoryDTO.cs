using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Shared.DTOs.Common;
using BlogEngine.Shared.DTOs.Abstractions;

namespace BlogEngine.Shared.DTOs.Category
{
    public class CategoryDTO : ReadDataDTOBase, IGenerateHATEOASLinks
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] GeneralCover { get; set; }

        public List<LinkDTO> LinkDTOs { get; set; } = new List<LinkDTO>();

        public void GenerateLinkDTOs(IUrlHelper urlHelper)
        {
            LinkDTOs.Add(new LinkDTO(urlHelper.Link("getCategory", new { ID }), "get-category", "GET"));
            LinkDTOs.Add(new LinkDTO(urlHelper.Link("updateCategory", new { ID }), "put-category", "PUT"));
            LinkDTOs.Add(new LinkDTO(urlHelper.Link("deleteCategory", new { ID }), "delete-category", "DELETE"));
        }

        public ResourceCollectionDTO<CategoryDTO> GenerateLinksCollectionDTO<CategoryDTO>(List<CategoryDTO> dtos, IUrlHelper urlHelper)
        {
            var resourceCollectionDTO = new ResourceCollectionDTO<CategoryDTO>(dtos);

            resourceCollectionDTO.LinkDTOs.Add(new LinkDTO(urlHelper.Link("getCategories", null), "self", "GET"));
            resourceCollectionDTO.LinkDTOs.Add(new LinkDTO(urlHelper.Link("createCategory", null), "create-category", "POST"));

            return resourceCollectionDTO;
        }
    }
}