using System;
using Nancy;
using Nancy.Bootstrapper;

namespace Hydra.Nancy
{
    /// <summary>
    /// Initialized the application for serving hydra API documentation
    /// </summary>
    public class HydraDocumentationStartup : IApplicationStartup
    {
        private readonly string _documentationPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraDocumentationStartup"/> class.
        /// </summary>
        /// <param name="settings">The hydra settings.</param>
        public HydraDocumentationStartup(IHydraDocumentationSettings settings)
        {
            _documentationPath = settings.DocumentationPath;
        }

        /// <summary>
        /// Performs Hydra initialization tasks
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendHydraHeader(_documentationPath));
        }

        private static Action<NancyContext> AppendHydraHeader(string documentationPath)
        {
            return context =>
            {
                var apiDocUri = context.Request.Url.SiteBase + context.Request.Url.BasePath + documentationPath;

                context.Response.AppendLinkHeader(apiDocUri, Hydra.apiDocumentation);
            };
        }
    }
}