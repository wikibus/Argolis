using System;

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
            return context => context.Response.Headers.Add("Link", string.Format(HydraHeaderFormat, apiDocUri));
        }
    }
}
