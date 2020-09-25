namespace BlogEngine.ClientServices.Helpers
{
    public class PaginatedResponse<TResponse>
    {
        public TResponse Response { get; set; }
        public int TotalAmountPages { get; set; }
    }
}