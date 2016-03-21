using Hydra.Discovery;
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
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings, IApiDocumentationFactory buidler)
        {
            Get[settings.DocumentationPath] = route =>
            {
                var apiDocumentation = buidler.Create();
                apiDocumentation.Id = Request.GetApiDocumentationUri(settings.DocumentationPath);
                return apiDocumentation;
            };
        }
    }
}
