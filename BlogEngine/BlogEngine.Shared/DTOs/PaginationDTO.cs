namespace BlogEngine.Shared.DTOs
{
    public class PaginationDTO
    {
        public PaginationDTO()
        { }

        public PaginationDTO(int page, int recordsPerPage)
        {
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
    }
}