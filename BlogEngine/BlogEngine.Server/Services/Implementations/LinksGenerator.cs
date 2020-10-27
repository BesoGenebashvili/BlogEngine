using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Helpers;

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

            if (model == null)
            {
                var listOfModels = objectResult.Value as List<T>;

                if (listOfModels == null)
                {
                    Throw.ArgumentNullException($"Was expecting an instance of {typeof(T)}");
                }

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