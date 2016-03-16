using Hydra.Core;
using Nancy;

namespace Hydra.Nancy
{
    /// <summary>
    /// Serves Hydra API documentation
    /// </summary>
    public class HydraApiDocumentationModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraApiDocumentationModule"/> class.
        /// </summary>
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings)
        {
            Get[settings.DocumentationPath] = route =>
            {
                var apiDocumentation = new ApiDocumentation(settings.EntryPoint);

                apiDocumentation.Id = Request.GetApiDocumentationUri(settings.DocumentationPath);

                return apiDocumentation;
            };
        }
    }
}
