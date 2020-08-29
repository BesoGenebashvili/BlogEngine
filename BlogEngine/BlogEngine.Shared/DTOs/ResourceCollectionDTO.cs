using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class ResourceCollectionDTO<TDTO>
    {
        public List<TDTO> ValueDTOs { get; set; }
        public List<LinkDTO> LinkDTOs { get; set; } = new List<LinkDTO>();

        public ResourceCollectionDTO(List<TDTO> valueDTOs)
        {
            ValueDTOs = valueDTOs;
        }
    }
}