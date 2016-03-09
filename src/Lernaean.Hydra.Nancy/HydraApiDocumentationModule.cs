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
        protected HydraApiDocumentationModule(IHydraDocumentationSettings settings, IApiDocumentationProvider apiDocumentationProvider)
        {
            Get[settings.DocumentationPath] = route => apiDocumentationProvider.CreateApiDocumentation();
        }
    }
}
