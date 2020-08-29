using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs.Abstractions
{
    public interface IGenerateHATEOASLinks
    {
        void GenerateLinkDTOs(IUrlHelper urlHelper);
        ResourceCollectionDTO<T> GenerateLinksCollectionDTO<T>(List<T> dtos, IUrlHelper urlHelper);
    }
}