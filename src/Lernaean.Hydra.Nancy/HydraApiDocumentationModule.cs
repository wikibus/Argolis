using System.Collections.Generic;
using System.Linq;
using Hydra.Annotations;
using Hydra.Core;
using Hydra.DocumentationDiscovery;
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
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings, ISupportedClassFactory factory)
        {
            Get[settings.DocumentationPath] = route =>
            {
                var apiDocumentation = new ApiDocumentation(settings.EntryPoint);

                apiDocumentation.Id = Request.GetApiDocumentationUri(settings.DocumentationPath);

                apiDocumentation.SupportedClasses = settings.Sources.SelectMany(source => source.FindTypes()).Select(factory.Create);

                return apiDocumentation;
            };
        }
    }
}
