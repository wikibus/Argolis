﻿using System;
using JsonLD.Entities.Context;
using Nancy;
using Nancy.Bootstrapper;

namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Initializes the application for serving hydra API documentation
    /// </summary>
    public class HydraDocumentationStartup : IApplicationStartup
    {
        private readonly string documentationPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraDocumentationStartup"/> class.
        /// </summary>
        /// <param name="settings">The hydra settings.</param>
        public HydraDocumentationStartup(IHydraDocumentationSettings settings)
        {
            this.documentationPath = settings.DocumentationPath;
        }

        /// <summary>
        /// Performs Hydra initialization tasks
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendHydraHeader(this.documentationPath));
        }

        private static Action<NancyContext> AppendHydraHeader(string documentationPath)
        {
            return context =>
            {
                string apiDocPath = context.Request.GetApiDocumentationUri(documentationPath);
                context.Response.AppendLinkHeader(apiDocPath, Vocab.Hydra.apiDocumentation);
            };
        }
    }
}
