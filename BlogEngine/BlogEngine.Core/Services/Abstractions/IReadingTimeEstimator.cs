namespace BlogEngine.Core.Services.Abstractions
{
    public interface IReadingTimeEstimator
    {
        int GetEstimatedReadingTime(string rawHtmlContent);
    }
}