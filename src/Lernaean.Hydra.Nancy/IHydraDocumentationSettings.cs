using System;
using System.Collections.Generic;
using Hydra.Core;
using Hydra.DocumentationDiscovery;
using JsonLD.Entities;

namespace Hydra.Nancy
{
    /// <summary>
    /// Settings for serving hydra API Documentation
    /// </summary>
    public interface IHydraDocumentationSettings
    {
        /// <summary>
        /// Gets the API Documentation path.
        /// </summary>
        string DocumentationPath { get; }

        /// <summary>
        /// Gets the sources, which will discover <see cref="Type"/>s to document
        /// in the <see cref="ApiDocumentation"/>.
        /// </summary>
        IEnumerable<ISupportedClassSource> Sources { get; }

        /// <summary>
        /// Gets the entry point URI.
        /// </summary>
        IriRef EntryPoint { get; }
    }
}
