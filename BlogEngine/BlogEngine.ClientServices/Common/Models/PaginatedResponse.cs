namespace BlogEngine.ClientServices.Common.Models
{
    public class PaginatedResponse<TResponse>
    {
        public TResponse Response { get; set; }
        public int TotalAmountPages { get; set; }
    }
}