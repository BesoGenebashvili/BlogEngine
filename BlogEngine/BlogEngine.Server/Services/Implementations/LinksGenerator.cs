using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Shared.Helpers;
using BlogEngine.Shared.DTOs.Abstractions;

namespace BlogEngine.Server.Services.Implementations
{
    public class LinksGenerator : ILinksGenerator
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        public LinksGenerator(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        public async Task Generate<T>(ResultExecutingContext context, ResultExecutionDelegate next)
            where T : class, IGenerateHATEOASLinks, new()
        {
            var urlHelper = GetUrlHelper();
            var objectResult = context.Result as ObjectResult;
            var model = objectResult.Value as T;

            if (model is null)
            {
                var listOfModels = objectResult.Value as List<T>;

                Preconditions.NotNull(listOfModels, typeof(T).Name);

                var instance = new T();

                objectResult.Value = instance.GenerateLinksCollectionDTO(listOfModels, urlHelper);

                listOfModels.ForEach(m => m.GenerateLinkDTOs(urlHelper));

                await next();
            }
            else
            {
                model.GenerateLinkDTOs(urlHelper);
                await next();
            }
        }

        private IUrlHelper GetUrlHelper()
        {
            return _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        }
    }
}