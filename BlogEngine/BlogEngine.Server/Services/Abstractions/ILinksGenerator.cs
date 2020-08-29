using BlogEngine.Shared.DTOs.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ILinksGenerator
    {
        Task Generate<T>(ResultExecutingContext context, ResultExecutionDelegate next) 
            where T : class, IGenerateHATEOASLinks, new();
    }
}