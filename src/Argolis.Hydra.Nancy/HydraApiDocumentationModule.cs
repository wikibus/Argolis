using Argolis.Hydra.Discovery;
using Nancy;

namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Serves Hydra API documentation
    /// </summary>
    public class HydraApiDocumentationModule : NancyModule
    {
        private readonly IHydraDocumentationSettings settings;
        private readonly IApiDocumentationFactory builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraApiDocumentationModule"/> class.
        /// </summary>
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings, IApiDocumentationFactory builder)
        {
            this.settings = settings;
            this.builder = builder;

            this.Get(settings.DocumentationPath, _ => this.GetDocumentation());
        }

        private dynamic GetDocumentation()
        {
            var apiDocumentation = this.builder.Create();
            apiDocumentation.Id = this.Request.GetApiDocumentationUri(this.settings.DocumentationPath);
            return apiDocumentation;
        }
    }
}
