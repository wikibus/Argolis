using Hydra.Discovery;
using Nancy;

namespace Hydra.Nancy
{
    /// <summary>
    /// Serves Hydra API documentation
    /// </summary>
    public class HydraApiDocumentationModule : NancyModule
    {
        private readonly IHydraDocumentationSettings _settings;
        private readonly IApiDocumentationFactory _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraApiDocumentationModule"/> class.
        /// </summary>
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings, IApiDocumentationFactory builder)
        {
            _settings = settings;
            _builder = builder;

            Get(settings.DocumentationPath, _ => GetDocumentation());
        }

        private dynamic GetDocumentation()
        {
            var apiDocumentation = _builder.Create();
            apiDocumentation.Id = Request.GetApiDocumentationUri(_settings.DocumentationPath);
            return apiDocumentation;
        }
    }
}
