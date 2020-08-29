using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BlogEngine.Server.Attributes
{
    public class CategoryHATEOASAttribute : HATEOASAttribute
    {
        private readonly ILinksGenerator _linksGenerator;

        public CategoryHATEOASAttribute(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var includeHATEOAS = ShouldIncludeHATEOAS(context);

            if (!includeHATEOAS)
            {
                await next();
                return;
            }

            await _linksGenerator.Generate<CategoryDTO>(context, next);
        }
    }
}