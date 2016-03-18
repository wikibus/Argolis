using System;
using System.Linq;
using System.Reflection;
using Hydra.Core;
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
        public HydraApiDocumentationModule(IHydraDocumentationSettings settings, ApiDocumentationFactory buidler)
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
