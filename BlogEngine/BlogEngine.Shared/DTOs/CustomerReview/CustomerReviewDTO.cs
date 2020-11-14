using BlogEngine.Shared.DTOs.Common;

namespace BlogEngine.Shared.DTOs.CustomerReview
{
    public class CustomerReviewDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int Rate { get; set; }
        public string Message { get; set; }
    }
}