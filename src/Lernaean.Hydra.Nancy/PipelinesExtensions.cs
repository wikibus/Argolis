using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Routing;

namespace Hydra.Nancy
{
    /// <summary>
    /// Wires Hydra with the application
    /// </summary>
    public static class PipelinesExtensions
    {
        private const string HydraHeaderFormat = "<{0}>; rel=\"" + Hydra.apiDocumentation + "\"";

        /// <summary>
        /// Wires Hydra documentation with Nancy pipeline
        /// </summary>
        public static void UseHydra(this IPipelines pipelines, string documentationPath)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendHydraHeader(documentationPath));
        }

        private static Action<NancyContext> AppendHydraHeader(string documentationPath)
        {
            return context =>
            {
                var apiDocUri = new Uri(context.Request.Url.SiteBase + context.Request.Url.BasePath + documentationPath);

                if (context.Response.Headers.ContainsKey("Link"))
                {
                    string current = context.Response.Headers["Link"];
                    context.Response.WithHeader("Link", current + ", " + string.Format(HydraHeaderFormat, apiDocUri));
                }
                else
                {
                    context.Response.WithHeader("Link", string.Format(HydraHeaderFormat, apiDocUri));
                }
            };
        }
    }

    public class HydraDocumentationStartup : IApplicationStartup
    {
        private const string HydraHeaderFormat = "<{0}>; rel=\"" + Hydra.apiDocumentation + "\"";

        public HydraDocumentationStartup(IHydraDocumentationSettings settings)
        {
            DocumentationPath = settings.DocumentationPath;
        }

        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendHydraHeader(DocumentationPath));
        }

        public string DocumentationPath { get; }

        private static Action<NancyContext> AppendHydraHeader(string documentationPath)
        {
            return context =>
            {
                var apiDocUri = new Uri(context.Request.Url.SiteBase + context.Request.Url.BasePath + documentationPath);

                if (context.Response.Headers.ContainsKey("Link"))
                {
                    string current = context.Response.Headers["Link"];
                    context.Response.WithHeader("Link", current + ", " + string.Format(HydraHeaderFormat, apiDocUri));
                }
                else
                {
                    context.Response.WithHeader("Link", string.Format(HydraHeaderFormat, apiDocUri));
                }
            };
        }
    }
}
