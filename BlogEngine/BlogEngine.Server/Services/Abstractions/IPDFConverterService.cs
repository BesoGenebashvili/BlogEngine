using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IPDFGenerator
    {
        Task<byte[]> GeneratePDFAsync(string htmlContent);
    }
}