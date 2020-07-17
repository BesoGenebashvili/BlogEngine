namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IReadingTimeEstimator
    {
        int GetEstimatedReadingTime(string rawHtmlContent);
    }
}