using System;
using Nancy.Bootstrapper;

namespace Nancy.Hydra
{
    /// <summary>
    /// Wires Hydra with the application
    /// </summary>
    public static class PipelinesExtensions
    {
        private const string HydraHeaderFormat = "<{0}>; rel=\"http://www.w3.org/ns/hydra/core#apiDocumentation\"";

        /// <summary>
        /// Wires Hydra documentation with Nancy pipeline
        /// </summary>
        public static void UseHydra(this IPipelines pipelines, Uri apiDocUri)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendHydraHeader(apiDocUri));
        }

        private static Action<NancyContext> AppendHydraHeader(Uri apiDocUri)
        {
            return context =>
            {
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
