using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions.Utilities
{
    public interface IPDFGenerator
    {
        Task<byte[]> GeneratePDFAsync(string htmlContent);
    }
}